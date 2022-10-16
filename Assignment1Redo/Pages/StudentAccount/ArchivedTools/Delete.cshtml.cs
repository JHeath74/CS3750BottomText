using BottomTextLMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BottomTextLMS.Pages.StudentAccount
{
    public class DeleteModel : PageModel
    {
        private readonly BottomTextLMS.AppDbContext _context;

        public DeleteModel(BottomTextLMS.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CreditCard CreditCard { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CreditCard = await _context.CreditCard.FirstOrDefaultAsync(m => m.ID == id);

            if (CreditCard == null)
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

            CreditCard = await _context.CreditCard.FindAsync(id);

            if (CreditCard != null)
            {
                _context.CreditCard.Remove(CreditCard);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
