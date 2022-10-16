using BottomTextLMS.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BottomTextLMS.Pages.FileUpload
{
    public class EditModel : PageModel
    {
        private readonly BottomTextLMS.AppDbContext _context;
        private readonly IWebHostEnvironment _iweb;

        public EditModel(BottomTextLMS.AppDbContext context, IWebHostEnvironment iweb)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(IFormFile uploadfiles, ProfileImage profileImage, string name, int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ProfileImage).State = EntityState.Modified;

            try
            {
                var ID = await _context.ProfileImage.FindAsync(id);
                _context.ProfileImage.Remove(ID);

                name = Path.Combine(_iweb.WebRootPath, "Files/ProfileImage", ID.ProfileName);
                FileInfo fi = new FileInfo(name);
                if (fi.Exists)
                {
                    System.IO.File.Delete(name);
                    fi.Delete();
                }

                string imgext = Path.GetExtension(uploadfiles.FileName);
                if (imgext == ".jpg" || imgext == ".gif")
                {
                    var imagesave = Path.Combine(_iweb.WebRootPath, "Files/ProfileImage", uploadfiles.FileName);
                    var stream = new FileStream(imagesave, FileMode.Create);
                    await uploadfiles.CopyToAsync(stream);
                    stream.Close();


                    ID.ID = id;
                    ID.ProfileName = uploadfiles.FileName;
                    ID.ProfileLocation = imagesave;
                    _context.Update(profileImage);
                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfileImageExists(ProfileImage.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/ProfileImageUpload/UploadImage");
        }

        private bool ProfileImageExists(int id)
        {
            return _context.ProfileImage.Any(e => e.ID == id);
        }
    }
}
