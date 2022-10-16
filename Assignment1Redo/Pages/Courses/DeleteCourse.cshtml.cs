using BottomTextLMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BottomTextLMS.Pages.Courses
{
    public class DeleteModel : PageModel
    {
        private readonly BottomTextLMS.AppDbContext _context;

        public DeleteModel(BottomTextLMS.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Class Class { get; set; }

        public User myProf { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, int? courseID)
        {
            if (id == null || courseID == null)
            {
                return NotFound();
            }

            Class = await _context.Classes
                .Include(m => m.Department)
                .Include(m => m.Room)
                .Include(m => m.User).FirstOrDefaultAsync(m => m.ID == courseID);

            myProf = await _context.Users.FirstOrDefaultAsync(u => u.ID == id);

            if (Class == null || myProf == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Class = await _context.Classes.FindAsync(id);
            int instructID = Class.InstructorID;

            if (Class != null)
            {
                _context.Classes.Remove(Class);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./CourseList", new { id = instructID });
        }
    }
}
