using BottomTextLMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace BottomTextLMS.Pages.StudentAssignments
{
    public class CreateModel : PageModel
    {
        private readonly BottomTextLMS.AppDbContext _context;

        public CreateModel(BottomTextLMS.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["ClassID"] = new SelectList(_context.Classes, "ID", "ClassName");
            return Page();
        }

        [BindProperty]
        public Assignment Assignment { get; set; }
        [BindProperty]
        public Submission mySubmission { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

           /* _context.Assignments.Add(Assignment);
            await _context.SaveChangesAsync();*/

            return RedirectToPage("./Index");
        }

        /*public async Task SaveTextSubmissions(Submission myTextSubmission)
        {
             _context.Submissions.Update(myTextSubmission);
             await _context.SaveChangesAsync();

        }*/
    }
}
