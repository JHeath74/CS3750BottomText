using BottomTextLMS.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading.Tasks;

namespace BottomTextLMS.Pages.FileUpload

{
    public class DeleteModel : PageModel
    {
        private readonly BottomTextLMS.AppDbContext _context;
        private readonly IWebHostEnvironment _iweb;

        public DeleteModel(BottomTextLMS.AppDbContext context, IWebHostEnvironment iweb)
        {
            _context = context;
            _iweb = iweb;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id, string name)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProfileImage = await _context.ProfileImage.FindAsync(id);
            _context.ProfileImage.Remove(ProfileImage);

            name = Path.Combine(_iweb.WebRootPath, "Files/ProfileImage", ProfileImage.ProfileName);
            FileInfo fi = new FileInfo(name);
            if (fi.Exists)
            {
                System.IO.File.Delete(name);
                fi.Delete();
            }

            if (ProfileImage != null)
            {
                _context.ProfileImage.Remove(ProfileImage);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/ProfileImageUpload/UploadImage");
        }
    }
}
