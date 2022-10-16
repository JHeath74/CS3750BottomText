using BottomTextLMS.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BottomTextLMS.Pages.Submissions
{
    public class AllSubmissionsModel : PageModel
    {
        public readonly BottomTextLMS.AppDbContext _context;

        public AllSubmissionsModel(BottomTextLMS.AppDbContext context)
        {
            _context = context;
        }

        public User prof { get; set; }

        public User currentStudent { get; set; }

        public IList<Submission> Submission { get; set; }

        public List<User> StudentNames { get; set; }

        public async Task OnGetAsync(int? id, int? assignID)
        {
            prof = await _context.Users.FirstOrDefaultAsync(a => a.ID == id);

            Submission = await _context.Submissions.Include(s => s.Assignment).Where(s => s.AssignmentID == assignID).ToListAsync();
        }
    }
}
