using BottomTextLMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BottomTextLMS.Pages.Assignments
{
    public class IndexModel : PageModel
    {
        private readonly BottomTextLMS.AppDbContext _context;

        public IndexModel(BottomTextLMS.AppDbContext context)
        {
            _context = context;
        }

        struct GradeInfo
        {
            public int MaxPoints { get; set; }
            public int? PointsEarned { get; set; }

            public int StudentID { get; set; }
        }

        public IList<Assignment> Assignment { get; set; }
        public User instructor { get; set; }
        public Class course { get; set; }

        [BindProperty]
        public Dictionary<string, int> grades { get; set; }

        public async Task<IActionResult> OnGetAsync(int id, int courseID)
        {
            instructor = await _context.Users.FirstOrDefaultAsync(a => a.ID == id);
            course = await _context.Classes.FirstOrDefaultAsync(a => a.ID == courseID);

            if (instructor == null || course == null)
            {
                return NotFound();
            }

            Assignment = await _context.Assignments.Where(a => a.ClassID == course.ID).ToListAsync();

            var result = from Arow in _context.Assignments
                         join SRow in _context.Submissions on Arow.ID equals SRow.AssignmentID
                         where Arow.ClassID == courseID
                         select new GradeInfo { MaxPoints = Arow.MaxPoints, PointsEarned = SRow.PointsEarned, StudentID = SRow.StudentID };

            var list = result.AsEnumerable()
                          .Select(o => new GradeInfo
                          {
                              MaxPoints = o.MaxPoints,
                              PointsEarned = o.PointsEarned,
                              StudentID = o.StudentID
                          }).ToList();

            List<int> students = new List<int>();

            foreach (GradeInfo item in list)
            {
                if (students.Contains(item.StudentID))
                {
                    continue;
                }
                else
                {
                    students.Add(item.StudentID);
                }
            }

            grades = new Dictionary<string, int>();

            grades.Add("A", 0);
            grades.Add("A-", 0);
            grades.Add("B+", 0);
            grades.Add("B", 0);
            grades.Add("B-", 0);
            grades.Add("C+", 0);
            grades.Add("C", 0);
            grades.Add("C-", 0);
            grades.Add("D+", 0);
            grades.Add("D", 0);
            grades.Add("D-", 0);
            grades.Add("E", 0);

            foreach (int student in students)
            {
                int maxPoints = 0;
                int? pointsEarned = 0;
                foreach (GradeInfo item in list)
                {
                    if (item.StudentID == student)
                    {
                        if (item.PointsEarned == null)
                        {
                            continue;
                        }
                        else
                        {
                            maxPoints += item.MaxPoints;
                            pointsEarned += item.PointsEarned;
                        }
                    }
                }
                float percentage = ((float)pointsEarned / (float)maxPoints) * 100;
                if (percentage <= 100.0 && percentage >= 94.0)
                {
                    grades["A"] += 1;
                }
                else if (percentage < 94.0 && percentage >= 90.0)
                {
                    grades["A-"] += 1;
                }
                else if (percentage < 90.0 && percentage >= 87.0)
                {
                    grades["B+"] += 1;
                }
                else if (percentage < 87.0 && percentage >= 84.0)
                {
                    grades["B"] += 1;
                }
                else if (percentage < 84.0 && percentage >= 80.0)
                {
                    grades["B-"] += 1;
                }
                else if (percentage < 80.0 && percentage >= 77.0)
                {
                    grades["C+"] += 1;
                }
                else if (percentage < 77.0 && percentage >= 74.0)
                {
                    grades["C"] += 1;
                }
                else if (percentage < 74.0 && percentage >= 70.0)
                {
                    grades["C-"] += 1;
                }
                else if (percentage < 70.0 && percentage >= 67.0)
                {
                    grades["D+"] += 1;
                }
                else if (percentage < 67.0 && percentage >= 64.0)
                {
                    grades["D"] += 1;
                }
                else if (percentage < 64.0 && percentage >= 60.0)
                {
                    grades["D-"] += 1;
                }
                else
                {
                    grades["E"] += 1;
                }
            }

            return Page();
        }
    }
}
