using BottomTextLMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BottomTextLMS.Pages.DataChart
{
    public class DataChartModel : PageModel
    {
        private readonly BottomTextLMS.AppDbContext _context;

        public DataChartModel(BottomTextLMS.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public User myUser { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            myUser = await _context.Users.FirstOrDefaultAsync(m => m.ID == id);

            return Page();
        }
    }
}
