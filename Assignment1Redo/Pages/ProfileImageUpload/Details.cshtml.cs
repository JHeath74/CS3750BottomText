using BottomTextLMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BottomTextLMS.Pages.FileUpload
{
    public class DetailsModel : PageModel
    {
        private readonly BottomTextLMS.AppDbContext _context;


        public DetailsModel(BottomTextLMS.AppDbContext context)
        {
            _context = context;

        }

        public ProfileImage ProfileImage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProfileImage = await _context.ProfileImage.FirstOrDefaultAsync(m => m.ID == id);


            if (ProfileImage == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
