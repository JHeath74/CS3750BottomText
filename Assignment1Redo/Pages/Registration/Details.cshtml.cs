using BottomTextLMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BottomTextLMS.Pages.Registration
{
    public class DetailsModel : PageModel
    {
        private readonly BottomTextLMS.AppDbContext _context;

        public DetailsModel(BottomTextLMS.AppDbContext context)
        {
            _context = context;
        }

        public Class Class { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Class = await _context.Classes
                .Include(m => m.Department)
                .Include(m => m.Room)
                .Include(m => m.User).FirstOrDefaultAsync(m => m.ID == id);

            if (Class == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
