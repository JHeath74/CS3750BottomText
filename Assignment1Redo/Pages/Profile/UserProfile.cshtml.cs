using BottomTextLMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;


namespace BottomTextLMS.Pages
{
    public class UserProfileModel : PageModel
    {
        private readonly BottomTextLMS.AppDbContext _context;

        public UserProfileModel(BottomTextLMS.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public User myUser { get; set; }

        [BindProperty]
        public User test { get; set; }

        [BindProperty]
        public BottomTextLMS.Models.UserInfo myInfo { get; set; }


        public UserInfo userInfo { get; set; }

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

<<<<<<< Updated upstream
=======


>>>>>>> Stashed changes
            return Page();
        }

        /*public async Task<IActionResult> OnPostAsync()
        {
            return RedirectToPage("/Profile/UserProfile" + "?id=" + myUser.ID);
        }*/

        public async void OnPostSaveProfile(int id, string returnUrl = null)
        {
<<<<<<< Updated upstream
=======


>>>>>>> Stashed changes
            //var OldInfo = _context.UsersInfo.Where(s => s.UserID == saveUser.ID).First();
            //OldInfo.Bio = "I changed the bio";

            User saveUser = _context.Users.FirstOrDefault(m => m.Email == myUser.Email);

            _context.Database.ExecuteSqlRaw(string.Format("UPDATE Users SET FirstName = '{0}' WHERE ID = {1}", myUser.FirstName, saveUser.ID));
            _context.Database.ExecuteSqlRaw(string.Format("UPDATE Users SET LastName = '{0}' WHERE ID = {1}", myUser.LastName, saveUser.ID));
            _context.Database.ExecuteSqlRaw(string.Format("UPDATE UsersInfo SET AddressLine1 = '{0}' WHERE UserID = {1}", myInfo.AddressLine1, saveUser.ID));
            _context.Database.ExecuteSqlRaw(string.Format("UPDATE UsersInfo SET City = '{0}' WHERE UserID = {1}", myInfo.City, saveUser.ID));
            _context.Database.ExecuteSqlRaw(string.Format("UPDATE UsersInfo SET State = '{0}' WHERE UserID = {1}", myInfo.State, saveUser.ID));
            _context.Database.ExecuteSqlRaw(string.Format("UPDATE UsersInfo SET Zipcode = '{0}' WHERE UserID = {1}", myInfo.Zipcode, saveUser.ID));
            _context.Database.ExecuteSqlRaw(string.Format("UPDATE UsersInfo SET PhoneNumber = '{0}' WHERE UserID = {1}", myInfo.PhoneNumber, saveUser.ID));
            _context.Database.ExecuteSqlRaw(string.Format("UPDATE UsersInfo SET Bio = '{0}' WHERE UserID = {1}", myInfo.Bio, saveUser.ID));
            _context.Database.ExecuteSqlRaw(string.Format("UPDATE UsersInfo SET Link1 = '{0}' WHERE UserID = {1}", myInfo.Link1, saveUser.ID));
            _context.Database.ExecuteSqlRaw(string.Format("UPDATE UsersInfo SET Link2 = '{0}' WHERE UserID = {1}", myInfo.Link2, saveUser.ID));
            _context.Database.ExecuteSqlRaw(string.Format("UPDATE UsersInfo SET Link3 = '{0}' WHERE UserID = {1}", myInfo.Link3, saveUser.ID));

            Response.Redirect(@"UserProfile?id=" + id, true);
        }
    }
}