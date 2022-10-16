using BottomTextLMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BottomTextLMS.Pages.StudentAccount
{
    public class DetailsModel : PageModel
    {
        private readonly BottomTextLMS.AppDbContext _context;

        public DetailsModel(BottomTextLMS.AppDbContext context)
        {
            _context = context;
        }

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
    }
}
