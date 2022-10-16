using BottomTextLMS.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BottomTextLMS.Pages.Submissions
{
    public class GradeSubmissionModel : PageModel
    {
        private readonly BottomTextLMS.AppDbContext _context;
        private IWebHostEnvironment Environment;

        public GradeSubmissionModel(BottomTextLMS.AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            Environment = environment;
        }

        [BindProperty]
        public User prof { get; set; }

        [BindProperty]
        public Submission submission { get; set; }

        [BindProperty]
        public Assignment assignment { get; set; }

        public async Task<IActionResult> OnGetAsync(int id, int subID)
        {
            prof = await _context.Users.FirstOrDefaultAsync(p => p.ID == id);

            var submissionQuery = from s in _context.Submissions
                                  where s.ID == subID
                                  select s;

            submission = await submissionQuery.SingleAsync();

            var assignmentQuery = from a in _context.Assignments
                                  where a.ID == submission.AssignmentID
                                  select a;

            assignment = await assignmentQuery.SingleAsync();

            return Page();
        }

        public FileResult OnGetDownloadFile(string filename)
        {
            string path = Path.Combine(Environment.WebRootPath, "Files/StudentAssignmentSubmission/" + filename);

            byte[] bytes = System.IO.File.ReadAllBytes(path);

            return File(bytes, "application/octet-stream", filename);
        }

        public async Task<IActionResult> OnPostAsync(int id, int assignID, int subID)
        {
            var result = _context.Submissions.SingleOrDefault(b => b.ID == subID);
            if (result != null)
            {
                result.PointsEarned = submission.PointsEarned;

                // Get class to enter class name field for event
                Assignment assignment = await _context.Assignments.FindAsync(assignID);
                Class Class = await _context.Classes.FindAsync(assignment.ClassID);

                // Add graded assignment event
                Event newEvent = new Event();
                newEvent.StudentID = result.StudentID;
                newEvent.AssignmentName = assignment.Title;
                newEvent.ClassName = Class.ClassName;
                newEvent.EventType = "Graded";
                newEvent.HasViewed = false;
                newEvent.TimeCreated = System.DateTime.Now;

                _context.Events.Add(newEvent);

                await _context.SaveChangesAsync();
            }

            return RedirectToPage("AllSubmissions", new { id = prof.ID, assignID = assignID });
        }
    }
}
