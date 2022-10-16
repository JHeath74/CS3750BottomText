using BottomTextLMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BottomTextLMS.Pages.Account
{
    public class SignupModel : PageModel
    {
        private readonly BottomTextLMS.AppDbContext _context;

        public SignupModel(BottomTextLMS.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public User myUser { get; set; }

        [BindProperty]
        public BottomTextLMS.Models.UserInfo myInfo { get; set; }

        [BindProperty]
        public BottomTextLMS.Models.Instructor myInstructor { get; set; }


        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            /*DateTime currentDate = DateTime.Now;
            currentDate = currentDate.AddYears(-18);

            if (myUser.BirthDate > currentDate)
            {
                return Page();
            }*/
            string hash = @"!$^#brule&saf";
            string encrypted_password;

            var checkQuery = from validate in _context.Users where validate.Email.Equals(myUser.Email) select validate;

            var userList = checkQuery.ToList();

            //Check if user already exists
            if (userList.Count() > 0)
            {
                return Page();
            }

            byte[] data = UTF8Encoding.UTF8.GetBytes(myUser.Password);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                using (TripleDESCryptoServiceProvider tripleDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripleDes.CreateEncryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    encrypted_password = Convert.ToBase64String(results);
                }
            }

            myUser.Password = encrypted_password;


            _context.Users.Add(myUser);
            await _context.SaveChangesAsync();

            // save new entry (just id) in UsersInfo table
            myInfo.UserID = myUser.ID;
            _context.UsersInfo.Add(myInfo);
            await _context.SaveChangesAsync();

            // update instructor table if new user is instructor
            if (myUser.Role == "Instructor")
            {
                myInstructor.ID = myUser.ID;
                myInstructor.FirstName = myUser.FirstName;
                myInstructor.LastName = myUser.LastName;
                _context.Instructors.Add(myInstructor);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Index", new
            {
                id = myUser.ID
            });
        }
    }
}
