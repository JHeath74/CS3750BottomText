using BottomTextLMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
<<<<<<< Updated upstream
using Assignment1Redo;
using Assignment1Redo.Models;

namespace Assignment1Redo.Pages.StudentAssignments
=======
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BottomTextLMS.Pages.StudentAssignments
>>>>>>> Stashed changes
{
    public class TextSubmissionModel : PageModel
    {
        private readonly BottomTextLMS.AppDbContext _context;

        public TextSubmissionModel(BottomTextLMS.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Assignment Assignment { get; set; }
       
        [BindProperty]
        public Submission sub { get; set; }
        
        [BindProperty]
        public User student { get; set; }
        public Submission submission;

        public List<Submission> allSubmissions { get; set; }
        public int?[] classScores = new int?[50];
        public int gradeCount = 0;
        public int arrayCounter = 0;

        public async Task<IActionResult> OnGetAsync(int id, int assignID)
        {
            student = await _context.Users.FirstOrDefaultAsync(a => a.ID == id);

            if (student == null)
            {
                return NotFound();
            }

            allSubmissions = await _context.Submissions.Where(a => a.AssignmentID == assignID).ToListAsync();

            foreach (Submission sub in allSubmissions)
            {
                classScores[gradeCount] = sub.PointsEarned;
                gradeCount++;
            }

            Assignment = await _context.Assignments
                .Include(a => a.Class).FirstOrDefaultAsync(m => m.ID == assignID);

            if (Assignment == null)
            {
                return NotFound();
            }

            sub = await _context.Submissions.FirstOrDefaultAsync(a => a.StudentID == id && a.AssignmentID == assignID);

            return Page();
        }

<<<<<<< Updated upstream
        public async Task<IActionResult> OnPostAsync(int? subID, int? id, int? classID) {
            SaveTextSubmission(sub.TextSubmission, subID);

=======
        public async Task<IActionResult> OnPostAsync(int? subID, int? id, int? classID)
        {
            _context.Database.ExecuteSqlRaw(string.Format("UPDATE Submissions SET TextSubmission = '{0}' WHERE ID = '{1}'", sub.TextSubmission, subID));
            _context.Database.ExecuteSqlRaw(string.Format("UPDATE Submissions SET HasSubmitted = '1' WHERE ID = '{0}'", subID));
            _context.Database.ExecuteSqlRaw(string.Format("UPDATE Submissions SET SubmitTime = '{0}' WHERE ID = '{1}'", DateTime.Now, subID));

>>>>>>> Stashed changes
            return RedirectToPage("/StudentAssignments/AssignmentList", new { id = id, courseID = classID });
        }

        public void SaveTextSubmission(string subTextSubmission, int? subID) {
            _context.Database.ExecuteSqlRaw(string.Format("UPDATE Submissions SET TextSubmission = '{0}' WHERE ID = '{1}'", subTextSubmission, subID));
            _context.Database.ExecuteSqlRaw(string.Format("UPDATE Submissions SET HasSubmitted = '1' WHERE ID = '{0}'", subID));
            _context.Database.ExecuteSqlRaw(string.Format("UPDATE Submissions SET SubmitTime = '{0}' WHERE ID = '{1}'", DateTime.Now, subID));
        }
    }
}
