using BottomTextLMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BottomTextLMS.Pages.Submissions
{
    public class AllSubmissionsDeleteModel : PageModel
    {
        private readonly BottomTextLMS.AppDbContext _context;

        public AllSubmissionsDeleteModel(BottomTextLMS.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Submission = await _context.Submissions.FindAsync(id);

            if (Submission != null)
            {
                _context.Submissions.Remove(Submission);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
