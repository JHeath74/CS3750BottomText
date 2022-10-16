using BottomTextLMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace BottomTextLMS.Pages
{
    public class IndexModel : PageModel
    {
        public struct AssignmentInfo
        {
            public int classNum;
            public string assignmentTitle;
            public string departmentAbbr;
            public DateTime dueDate;
            public string assignmentType;
            public int assignmentID;
        }

        public struct ClassCard
        {
            public int id;
            public int classNum;
            public string className;
            public string instructor;
            public string building;
            public int roomNum;
            public string daysOfWeek;
            public string startTime;
            public string endTime;
        }

        private readonly BottomTextLMS.AppDbContext _context;
        private readonly ILogger<IndexModel> _logger;
        public User user { get; set; }
        public List<Class> classes { get; set; }
        public List<ClassCard> classCards { get; set; }

        public List<Class> list_classes { get; set; }

        public List<Assignment> list_assignments { get; set; }

        public List<AssignmentInfo> list_assignmentinfo { get; set; }


        public IndexModel(ILogger<IndexModel> logger, BottomTextLMS.AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            user = await _context.Users.FirstOrDefaultAsync(a => a.ID == id);

            if (user == null)
            {
                return NotFound();
            }

            // Instantiate classCards list
            classCards = new List<ClassCard>();

            // Get classes for user from memory cache
            var cacheItemKey = "User" + user.ID;
            List<Class> classes = WebCache.Get(cacheItemKey);

            // If no cache entry for user currently exists, query for classes and add to cache
            //      - Note: These queries should not run every time this page is navigated to.
            if (classes == null)
            {
                classes = new List<Class>();

                if (user.Role == "Instructor")
                {
                    classes = await _context.Classes.Where(m => m.InstructorID == user.ID).ToListAsync();
                }
                else if (user.Role == "Student")
                {
                    List<Enrollment> enrollments = await _context.Enrollments.Where(e => e.StudentID == user.ID).ToListAsync();

                    foreach (var e in enrollments)
                    {
                        Class classInfo = await _context.Classes.Where(c => c.ID == e.ClassID).SingleAsync();
                        classes.Add(classInfo);
                    }
                }

                WebCache.Set(cacheItemKey, classes);
            }


            // For each class, query for instructor name, room number, and building, then create new class card with info
            if (classes != null)
            {
                foreach (Class Class in classes)
                {
                    ClassCard newClassCard;

                    // Variables to hold instructor, room, and building info from queries
                    Room room;
                    User instructor;
                    Building building;

                    var roomQuery = from r in _context.Rooms
                                    where r.ID == Class.RoomID
                                    select r;

                    room = await roomQuery.SingleAsync();

                    var instructQuery = from i in _context.Users
                                        where i.ID == Class.InstructorID
                                        select i;

                    instructor = await instructQuery.SingleAsync();

                    var buildingQuery = from b in _context.Buildings
                                        where b.ID == room.BuildingID
                                        select b;

                    building = await buildingQuery.SingleAsync();

                    // After queries are executed, define Class Card
                    newClassCard.id = Class.ID;
                    newClassCard.classNum = (int)Class.ClassNumber;
                    newClassCard.className = Class.ClassName;
                    newClassCard.instructor = instructor.FirstName + " " + instructor.LastName;
                    newClassCard.building = building.BuildingName;
                    newClassCard.roomNum = (int)room.RoomNumber;
                    newClassCard.daysOfWeek = Class.DaysOfWeek;
                    newClassCard.startTime = Class.StartTime.ToString("hh:mm tt");
                    newClassCard.endTime = Class.EndTime.ToString("hh:mm tt");

                    classCards.Add(newClassCard);
                }
            }

            var classesQuery = from enrollmentRow in _context.Enrollments
                               join classRow in _context.Classes on enrollmentRow.ClassID equals classRow.ID
                               where enrollmentRow.StudentID == id
                               select classRow;

            list_classes = classesQuery.ToList<Class>();

            list_assignmentinfo = new List<AssignmentInfo>();

            foreach (Class _class in list_classes)
            {
                var assignmentsQuery = from classRow in _context.Classes
                                       join assignmentRow in _context.Assignments on classRow.ID equals assignmentRow.ClassID
                                       where classRow.ID == _class.ID && assignmentRow.DueDate >= DateTime.Today
                                       select assignmentRow;

                list_assignments = assignmentsQuery.ToList<Assignment>();

                var getDepartment = from classRow in _context.Classes
                                    join departmentRow in _context.Departments on classRow.DepartmentID equals departmentRow.ID
                                    where classRow.ID == _class.ID
                                    select departmentRow;

                Department department = getDepartment.SingleOrDefault();

                foreach (Assignment assignment in list_assignments)
                {
                    AssignmentInfo assignmentInfo;
                    assignmentInfo.classNum = _class.ClassNumber.Value;
                    assignmentInfo.assignmentTitle = assignment.Title;
                    assignmentInfo.dueDate = assignment.DueDate;
                    assignmentInfo.departmentAbbr = department.DepartmentAbbrv;
                    assignmentInfo.assignmentType = assignment.AssignmentType;
                    assignmentInfo.assignmentID = assignment.ID;

                    list_assignmentinfo.Add(assignmentInfo);
                }
            }

            // sorts assignmentinfo by due date
            list_assignmentinfo.Sort((x, y) => DateTime.Compare(x.dueDate, y.dueDate));

            return Page();
        }
    }
}
