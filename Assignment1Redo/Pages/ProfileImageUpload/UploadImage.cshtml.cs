using BottomTextLMS.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BottomTextLMS.Pages.FileUpload
{
    public class IndexModel : PageModel
    {
        private readonly BottomTextLMS.AppDbContext _context;
        private readonly IWebHostEnvironment _iweb;


        public IndexModel(BottomTextLMS.AppDbContext context, IWebHostEnvironment iweb)
        {
            _context = context;
            _iweb = iweb;
        }


        public UserInfo UserInfo { get; set; }


        [BindProperty]
        public User myUser { get; set; }

        [BindProperty]
        public BottomTextLMS.Models.UserInfo myInfo { get; set; }



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

        public async Task<IActionResult> OnPostAsync(IFormFile uploadfiles, UserInfo userinfo, int? id, int? classID)
        {


            //Get File Name, Extention and Location
            string fileExt = Path.GetExtension(uploadfiles.FileName);
            string FileName = uploadfiles.FileName;
            string Location = _iweb.WebRootPath;

            // Giving the file name a unique name
            FileName = Guid.NewGuid().ToString();
            var GUIDFileNameAndExtention = FileName + fileExt;

            //Setting Save location
            var SaveFileLocation = Path.Combine(_iweb.WebRootPath, "Files\\ProfileImage", GUIDFileNameAndExtention);
            var UserInformation = userinfo.UserID;

            //Saving file to save location
            var stream = new FileStream(SaveFileLocation, FileMode.Create);
            await uploadfiles.CopyToAsync(stream);
            stream.Close();

            //Writing information to database

            userinfo.ProfileImageName = GUIDFileNameAndExtention;

            _context.Database.ExecuteSqlRaw(string.Format("UPDATE UsersInfo SET ProfileImageName = '{0}' Where UserID = {1}", userinfo.ProfileImageName, userinfo.UserID));


            return RedirectToPage("/Profile/UserProfile", new { id = id, courseID = classID });

        }
    }
}
