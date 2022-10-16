using BottomTextLMS.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BottomTextLMS.Pages.StudentAssignments
{
    public class DetailsModel : PageModel
    {
        private readonly BottomTextLMS.AppDbContext _context;
        private readonly IWebHostEnvironment _iweb;

        public DetailsModel(BottomTextLMS.AppDbContext context, IWebHostEnvironment iweb)
        {
            _context = context;
            _iweb = iweb;
        }

        //public IList<Submission> submissions { get; set; }
        [BindProperty]
        public Submission submissions { get; set; }
        public Assignment Assignment { get; set; }
        public User student { get; set; }
        [BindProperty]
        public User myUser { get; set; }
        [BindProperty]
        public BottomTextLMS.Models.UserInfo myInfo { get; set; }

        [BindProperty]
        public Submission sub { get; set; }
        [BindProperty]
        public List<Submission> allSubmissions { get; set; }
        public int?[] classScores = new int?[50];
        public int gradeCount = 0;
        public int arrayCounter = 0;


        public async Task<IActionResult> OnGetAsync(int id, int assignID)
        {
            student = await _context.Users.FirstOrDefaultAsync(a => a.ID == id);

            if (student == null)
            {
                return NotFound();
            }

            Assignment = await _context.Assignments
                .Include(a => a.Class).FirstOrDefaultAsync(m => m.ID == assignID);

            if (Assignment == null)
            {
                return NotFound();
            }

            submissions = await _context.Submissions.FirstOrDefaultAsync(a => a.StudentID == id && a.AssignmentID == assignID);

            allSubmissions = await _context.Submissions.Where(a => a.AssignmentID == assignID).ToListAsync();

            foreach (Submission sub in allSubmissions)
            {
                classScores[gradeCount] = sub.PointsEarned;
                gradeCount++;
            }

            sub = await _context.Submissions.FirstOrDefaultAsync(a => a.StudentID == id && a.AssignmentID == assignID);


            return Page();
        }

        public async Task<IActionResult> OnPostAsync(IFormFile uploadAssignment, Submission submissions, int subID, int id, int classID)
        {

            if (uploadAssignment == null)
            {
                return Page();
            }

            submissions = await _context.Submissions.FirstOrDefaultAsync(a => a.ID == subID);

            //Get File Name, Extention and Location
            string fileExt = Path.GetExtension(uploadAssignment.FileName);
            string FileName = uploadAssignment.FileName;
            string Location = _iweb.WebRootPath;

            // Giving the file name a unique name
            FileName = Guid.NewGuid().ToString();
            var GUIDFileNameAndExtention = FileName + fileExt;

            //Setting Save location
            var SaveFileLocation = Path.Combine(_iweb.WebRootPath, "Files\\StudentAssignmentSubmission", GUIDFileNameAndExtention);


            //Saving file to save location
            var stream = new FileStream(SaveFileLocation, FileMode.Create);
            await uploadAssignment.CopyToAsync(stream);
            stream.Close();

            //Writing information to database
            submissions.FileSubmission = GUIDFileNameAndExtention;

            _context.Database.ExecuteSqlRaw(string.Format("UPDATE Submissions SET FileSubmission = '{0}' WHERE ID = {1}", submissions.FileSubmission, submissions.ID));
            _context.Database.ExecuteSqlRaw(string.Format("UPDATE Submissions SET HasSubmitted = '1' WHERE ID = {0}", submissions.ID));

            return RedirectToPage("/StudentAssignments/AssignmentList", new { id = id, courseID = classID });

        }
    }
}
