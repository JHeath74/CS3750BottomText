using BottomTextLMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BottomTextLMS.Pages.Registration
{
    public class RegistrationModel : PageModel
    {
        private readonly BottomTextLMS.AppDbContext _context;

        public RegistrationModel(BottomTextLMS.AppDbContext context)
        {
            _context = context;
        }

        // list of departments for dropdown
        public SelectList DepartmentList { get; set; }

        [BindProperty]
        public User myUser { get; set; }

        [BindProperty]
        public List<Class> Class { get; set; }

        public User student { get; set; }

        // text found in search box
        [BindProperty]
        public string ClassSearchText { get; set; }

        // dropdown department currently selected
        [BindProperty]
        public int selectedDept { get; set; }

        // list of all current enrollments (in the Enrollments table)
        [BindProperty]
        public List<Enrollment> currentEnrollment { get; set; }

        // flag used for enrollment logic
        [BindProperty]
        public bool isEnrolled { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            student = await _context.Users.FirstOrDefaultAsync(p => p.ID == id);


            currentEnrollment = await _context.Enrollments.ToListAsync();

            DepartmentList = new SelectList(_context.Departments, "ID", "DepartmentName");

            // selects all classes from Classes table
            Class = await _context.Classes
                .Include(m => m.Department)
                .Include(m => m.Room)
                .Include(m => m.User).ToListAsync();

            return Page();


        }

        public async Task<IActionResult> OnPostSearchCriteria(int id)
        {
            student = await _context.Users.FirstOrDefaultAsync(p => p.ID == id);

            currentEnrollment = await _context.Enrollments.ToListAsync();

            DepartmentList = new SelectList(_context.Departments, "ID", "DepartmentName");

            if (selectedDept == 0 && ClassSearchText == null)
            { // CASE: "Any Department" selected w/ no keyword search 
                Class = await _context.Classes
                .Include(m => m.Department)
                .Include(m => m.Room)
                .Include(m => m.User).ToListAsync();
            }
            else if (selectedDept == 0 && ClassSearchText != null)
            { // CASE: "Any Department" selected w/ a keyword search
                Class = await _context.Classes
                .Include(m => m.Department)
                .Include(m => m.Room)
                .Include(m => m.User).ToListAsync();

                foreach (Class item in Class.ToList())
                {
                    if (!(item.ClassName.IndexOf(ClassSearchText, StringComparison.OrdinalIgnoreCase) >= 0))
                    {
                        Class.Remove(item);
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            else
            { // CASE: specific department is selected with keyword search
                Class = await _context.Classes.Where(m => m.Department.ID == selectedDept).Include(m => m.Department).Include(m => m.Room).Include(m => m.User).ToListAsync();

                if (ClassSearchText != null)
                {
                    foreach (Class item in Class.ToList())
                    {
                        if (!(item.ClassName.IndexOf(ClassSearchText, StringComparison.OrdinalIgnoreCase) >= 0))
                        {
                            Class.Remove(item);
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostRegisterStudent(int clickedID, int id)
        {
            await RegisterStudent(id, clickedID);

            // update current enrollment list
            currentEnrollment = await _context.Enrollments.ToListAsync();

            DepartmentList = new SelectList(_context.Departments, "ID", "DepartmentName");

            if (selectedDept == 0 && ClassSearchText == null)
            { // CASE: "Any Department" selected w/ no keyword search 
                Class = await _context.Classes
                .Include(m => m.Department)
                .Include(m => m.Room)
                .Include(m => m.User).ToListAsync();
            }
            else if (selectedDept == 0 && ClassSearchText != null)
            { // CASE: "Any Department" selected w/ a keyword search
                Class = await _context.Classes
                .Include(m => m.Department)
                .Include(m => m.Room)
                .Include(m => m.User).ToListAsync();

                foreach (Class item in Class.ToList())
                {
                    if (!(item.ClassName.IndexOf(ClassSearchText, StringComparison.OrdinalIgnoreCase) >= 0))
                    {
                        Class.Remove(item);
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            else
            { // CASE: specific department is selected with keyword search
                Class = await _context.Classes.Where(m => m.Department.ID == selectedDept).Include(m => m.Department).Include(m => m.Room).Include(m => m.User).ToListAsync();

                if (ClassSearchText != null)
                {
                    foreach (Class item in Class.ToList())
                    {
                        if (!(item.ClassName.IndexOf(ClassSearchText, StringComparison.OrdinalIgnoreCase) >= 0))
                        {
                            Class.Remove(item);
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostDropStudent(int clickedID, int id, string keyword, int dept)
        {
<<<<<<< Updated upstream
            await DropStudent(id, clickedID);
=======
            student = await _context.Users.FirstOrDefaultAsync(p => p.ID == id);

            // delete student's enrollment from Enrollments table
            _context.Database.ExecuteSqlRaw(string.Format("DELETE FROM Enrollments WHERE StudentID = {0} AND ClassID = {1}", student.ID, clickedID));

            // delete all assignment submissions from this student
            List<Assignment> assignments = await _context.Assignments.Where(a => a.ClassID == clickedID).ToListAsync();

            foreach (Assignment assignment in assignments)
            {
                Submission submission = await _context.Submissions.Where(s => s.StudentID == student.ID && s.AssignmentID == assignment.ID).FirstOrDefaultAsync();
                if (submission != null)
                    _context.Submissions.Remove(submission);
            }
            await _context.SaveChangesAsync();
>>>>>>> Stashed changes

            // update current enrollment list
            currentEnrollment = await _context.Enrollments.ToListAsync();

            DepartmentList = new SelectList(_context.Departments, "ID", "DepartmentName");

            if (selectedDept == 0 && ClassSearchText == null)
            { // CASE: "Any Department" selected w/ no keyword search 
                Class = await _context.Classes
                .Include(m => m.Department)
                .Include(m => m.Room)
                .Include(m => m.User).ToListAsync();
            }
            else if (selectedDept == 0 && ClassSearchText != null)
            { // CASE: "Any Department" selected w/ a keyword search
                Class = await _context.Classes
                .Include(m => m.Department)
                .Include(m => m.Room)
                .Include(m => m.User).ToListAsync();

                foreach (Class item in Class.ToList())
                {
                    if (!(item.ClassName.IndexOf(ClassSearchText, StringComparison.OrdinalIgnoreCase) >= 0))
                    {
                        Class.Remove(item);
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            else
            { // CASE: specific department is selected with keyword search
                Class = await _context.Classes.Where(m => m.Department.ID == selectedDept).Include(m => m.Department).Include(m => m.Room).Include(m => m.User).ToListAsync();

                if (ClassSearchText != null)
                {
                    foreach (Class item in Class.ToList())
                    {
                        if (!(item.ClassName.IndexOf(ClassSearchText, StringComparison.OrdinalIgnoreCase) >= 0))
                        {
                            Class.Remove(item);
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }

            return Page();
        }

        public async Task RegisterStudent(int studentID, int classID)
        {
            student = await _context.Users.FirstOrDefaultAsync(p => p.ID == studentID);

            // create new enrollment object
            Enrollment newRegister = new Enrollment();
            newRegister.StudentID = student.ID;
            newRegister.ClassID = classID;

            // save new enrollment to db
            _context.Enrollments.Add(newRegister);

            // add this student's submissions for all class assignments
            List<Assignment> assignments = await _context.Assignments.Where(a => a.ClassID == classID).ToListAsync();

            foreach (Assignment assignment in assignments)
            {
                Submission newSubmission = new Submission();
                newSubmission.StudentID = student.ID;
                newSubmission.AssignmentID = assignment.ID;
                newSubmission.HasSubmitted = false;

                _context.Submissions.Add(newSubmission);
            }
            await _context.SaveChangesAsync();
        }

        public async Task DropStudent(int studentID, int classID)
        {
            student = await _context.Users.FirstOrDefaultAsync(p => p.ID == studentID);

            // delete student's enrollment from Enrollments table
            _context.Database.ExecuteSqlRaw(string.Format("DELETE FROM Enrollments WHERE StudentID = {0} AND ClassID = {1}", student.ID, classID));

            // delete all assignment submissions from this student
            List<Assignment> assignments = await _context.Assignments.Where(a => a.ClassID == classID).ToListAsync();

            foreach (Assignment assignment in assignments)
            {
                Submission submission = await _context.Submissions.Where(s => s.StudentID == student.ID && s.AssignmentID == assignment.ID).FirstOrDefaultAsync();
                if (submission != null)
                    _context.Submissions.Remove(submission);
            }
            await _context.SaveChangesAsync();
        }
    }
}