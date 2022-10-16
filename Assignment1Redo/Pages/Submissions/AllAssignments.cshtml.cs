using BottomTextLMS.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BottomTextLMS.Pages.Submissions
{
    public class IndexModel : PageModel
    {
        private readonly BottomTextLMS.AppDbContext _context;

        public IndexModel(BottomTextLMS.AppDbContext context)
        {
            _context = context;
        }


        public User Instructor { get; set; }

        public IList<Assignment> Assignment { get; set; }

        public async Task OnGetAsync(int? id)
        {
            Instructor = await _context.Users.FirstOrDefaultAsync(a => a.ID == id);

            Assignment = await _context.Assignments.Include(a => a.Class).Where(a => a.Class.InstructorID == Instructor.ID).ToListAsync();
        }
    }
}
