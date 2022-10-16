using BottomTextLMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BottomTextLMS.Pages.Assignments
{
    public class CreateModel : PageModel
    {
        private readonly BottomTextLMS.AppDbContext _context;

        public CreateModel(BottomTextLMS.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Assignment Assignment { get; set; }

        [BindProperty]
        public User prof { get; set; }

        [BindProperty]
        public int InstructID { get; set; }

        [BindProperty]
        public Class Class { get; set; }

        [BindProperty]
        public List<Enrollment> Enrollments { get; set; }

        public async Task<IActionResult> OnGetAsync(int id, int courseID)
        {
            prof = await _context.Users.FirstOrDefaultAsync(p => p.ID == id);
            Class = await _context.Classes.FirstOrDefaultAsync(c => c.ID == courseID);

            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(int courseID)
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            Class = await _context.Classes.FindAsync(courseID);

            await SaveAssignment(Assignment);

            List<Enrollment> thisClassEnrollments;

            thisClassEnrollments = await _context.Enrollments.Where(m => m.ClassID == courseID).ToListAsync();

            foreach (Enrollment enroll in thisClassEnrollments)
            {


                // Add created assignment event for each student
                Event newEvent = new Event();
                newEvent.StudentID = enroll.StudentID;
                newEvent.AssignmentName = Assignment.Title;
                newEvent.ClassName = Class.ClassName;
                newEvent.EventType = "Created";
                newEvent.HasViewed = false;
                newEvent.TimeCreated = System.DateTime.Now;

                _context.Events.Add(newEvent);

                // Add submission for each student
                Submission sub = new Submission();
                sub.StudentID = enroll.StudentID;
                sub.AssignmentID = Assignment.ID;
                sub.HasSubmitted = false;

                _context.Submissions.Add(sub);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Assignments/AssignmentList", new { id = InstructID, courseID = Assignment.ClassID });
        }

        public async Task SaveAssignment(Assignment Assignment)
        {
            _context.Assignments.Add(Assignment);
            await _context.SaveChangesAsync();
        }
    }
}
