using BottomTextLMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BottomTextLMS.Pages.StudentAssignments
{
    public class IndexModel : PageModel
    {
        private readonly BottomTextLMS.AppDbContext _context;

        public IndexModel(BottomTextLMS.AppDbContext context)
        {
            _context = context;
        }

        public IList<Assignment> Assignment { get; set; }
        public User student { get; set; }
        public Class course { get; set; }
        public int TotalPointsPossible { get; set; }
        public int PointsEarned { get; set; }
        public string LetterGrade { get; set; }

        public async Task<IActionResult> OnGetAsync(int id, int courseID)
        {
            student = await _context.Users.FirstOrDefaultAsync(a => a.ID == id);
            course = await _context.Classes.FirstOrDefaultAsync(a => a.ID == courseID);

            if (student == null || course == null)
            {
                return NotFound();
            }

            Assignment = await _context.Assignments.Where(a => a.ClassID == course.ID)
                .Include(a => a.Class)
                .Include(a => a.Submissions).ToListAsync();

            foreach (Assignment assign in Assignment)
            {
                foreach (Submission sub in assign.Submissions)
                {
                    if (sub.StudentID == id && sub.PointsEarned != null)
                    {
                        TotalPointsPossible += assign.MaxPoints;
                        PointsEarned += (int)sub.PointsEarned;
                    }
                }
            }

            // calculate letter grade
            double gradePercentage = ((double)PointsEarned / TotalPointsPossible) * 100;
            if (gradePercentage >= 94.0)
                LetterGrade = "A";
            else if (90.0 <= gradePercentage && gradePercentage < 94.0)
                LetterGrade = "A-";
            else if (87.0 <= gradePercentage && gradePercentage < 90.0)
                LetterGrade = "B+";
            else if (84.0 <= gradePercentage && gradePercentage < 87.0)
                LetterGrade = "B";
            else if (80.0 <= gradePercentage && gradePercentage < 84.0)
                LetterGrade = "B-";
            else if (77.0 <= gradePercentage && gradePercentage < 80.0)
                LetterGrade = "C+";
            else if (74.0 <= gradePercentage && gradePercentage < 77.0)
                LetterGrade = "C";
            else if (70.0 <= gradePercentage && gradePercentage < 74.0)
                LetterGrade = "C-";
            else if (67.0 <= gradePercentage && gradePercentage < 70.0)
                LetterGrade = "D+";
            else if (64.0 <= gradePercentage && gradePercentage < 67.0)
                LetterGrade = "D";
            else if (60.0 <= gradePercentage && gradePercentage < 64.0)
                LetterGrade = "D-";
            else if (gradePercentage < 60.0)
                LetterGrade = "E";
            else
                LetterGrade = "N/A";

            return Page();
        }
    }
}
