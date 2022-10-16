using BottomTextLMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace BottomTextLMS.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly BottomTextLMS.AppDbContext _context;

        public LoginModel(BottomTextLMS.AppDbContext context)
        {
            _context = context;
        }

        string hash = @"!$^#brule&saf";
        string encrypted_password;

        [BindProperty]
        public User myUser { get; set; }

        //public IList<User> userList { get; set; }
        public User user { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (myUser.Email == null || myUser.Password == null)
            {
                return Page();
            }

            /*var userQuery = from validate in _context.Users where validate.Email.Equals(myUser.Email) && validate.Password.Equals(myUser.Password) select validate;

            userList = userQuery.ToList();

            if (userList.Count() > 0)
            {
                return RedirectToPage("/Index");
            }

            return Page();*/

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

            var userQuery = from validate in _context.Users where validate.Email.Equals(myUser.Email) && validate.Password.Equals(encrypted_password) select validate;

            try
            {
                user = await userQuery.SingleAsync();
            }
            catch (Exception e)
            {
                return Page();
            }

            // -----------------STORE USER CLASS INFO IN CACHE ON LOG IN---------------
            List<Class> classes = new List<Class>();

            // Get list of classes for this user
            if (user.Role == "Instructor")
            {
                classes = await _context.Classes.Where(m => m.InstructorID == user.ID).ToListAsync();
            }
            else if (user.Role == "Student")
            {
                List<Enrollment> enrollments = await _context.Enrollments.Where(e => e.StudentID == user.ID).ToListAsync();

                foreach (var e in enrollments)
                {
                    Class classInfo = await _context.Classes.Where(c => c.ID == e.ClassID).SingleAsync();
                    classes.Add(classInfo);
                }
            }

            var cacheItemKey = "User" + user.ID;

            WebCache.Set(cacheItemKey, classes);

            // ------------------------------------------------------------------------

            // Get user notification events on log in
            if (user.Role == "Student")
            {
                // Remove already viewed events
                var viewedEvents = await _context.Events.Where(e => e.StudentID == user.ID && e.HasViewed).ToArrayAsync();
                _context.Events.RemoveRange(viewedEvents);

                // Get list of events that haven't been viewed
                var newEvents = await _context.Events.Where(e => e.StudentID == user.ID && !e.HasViewed).ToListAsync();

                // Mark all new events as now viewed (these events should be removed when user next logs in)
                foreach (var e in newEvents)
                {
                    e.HasViewed = true;
                }
                _context.Events.UpdateRange(newEvents);

                // Save database changes
                await _context.SaveChangesAsync();

                // Add list of events to ViewData dictionary
                var cacheEventsKey = "User" + user.ID + "Events";
                WebCache.Set(cacheEventsKey, newEvents);
            }

            return RedirectToPage("/Index", new
            {
                id = user.ID
            });

        }
    }
}
