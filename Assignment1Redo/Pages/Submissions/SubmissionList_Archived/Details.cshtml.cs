using BottomTextLMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BottomTextLMS.Pages.Submissions
{
    public class AllSubmissionsDetailsModel : PageModel
    {
        private readonly BottomTextLMS.AppDbContext _context;

        public AllSubmissionsDetailsModel(BottomTextLMS.AppDbContext context)
        {
            _context = context;
        }

        public Submission Submission { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Submission = await _context.Submissions
                .Include(s => s.Assignment).FirstOrDefaultAsync(m => m.ID == id);

            if (Submission == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
