using BottomTextLMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BottomTextLMS.Pages.FileUpload
{
    public class CreateModel : PageModel
    {
        private readonly BottomTextLMS.AppDbContext _context;

        public CreateModel(BottomTextLMS.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public User myUser { get; set; }

        /*  public IActionResult OnGet()
          {
              return Page();
          }*/

        [BindProperty]
        public BottomTextLMS.Models.UserInfo myInfo { get; set; }

        [BindProperty]
        public ProfileImage ProfileImage { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            myUser = await _context.Users.FirstOrDefaultAsync(m => m.ID == id);
            myInfo = await _context.UsersInfo.FirstOrDefaultAsync(m => m.UserID == id);

            if (myUser == null)
            {
                return NotFound();
            }

            return Page();
        }
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ProfileImage.Add(ProfileImage);
            await _context.SaveChangesAsync();

            return RedirectToPage("/ProfileImageUpload/UploadImage");
        }
    }
}
