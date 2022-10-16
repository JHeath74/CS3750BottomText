using BottomTextLMS.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BottomTextLMS.Pages.StudentAccount
{
    public class IndexModel : PageModel
    {
        private readonly BottomTextLMS.AppDbContext _context;

        public IndexModel(BottomTextLMS.AppDbContext context)
        {
            _context = context;
        }

        public IList<CreditCard> CreditCard { get; set; }

        public async Task OnGetAsync()
        {
            CreditCard = await _context.CreditCard.ToListAsync();
        }
    }
}
