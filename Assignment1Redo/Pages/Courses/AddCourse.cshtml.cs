using BottomTextLMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BottomTextLMS.Pages.Courses
{
    public class CreateModel : PageModel
    {
        private readonly BottomTextLMS.AppDbContext _context;

        public CreateModel(BottomTextLMS.AppDbContext context)
        {
            _context = context;

        }

        [BindProperty]
        public string[] Days { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            ViewData["DepartmentID"] = new SelectList(_context.Departments, "ID", "DepartmentName");
            ViewData["RoomID"] = new SelectList(_context.Rooms, "ID", "RoomNumber");
            ViewData["BuildingID"] = new SelectList(_context.Buildings, "ID", "BuildingName");

            Days = new string[5];

            prof = await _context.Users.FirstOrDefaultAsync(p => p.ID == id);

            return Page();
        }

        [BindProperty]
        public Class Class { get; set; }

        public User prof { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            string DaysString = String.Join(",", Days.Where(s => s != "false"));
            Class.DaysOfWeek = DaysString;
            
            await SaveCourse(Class);

            return RedirectToPage("/Courses/CourseList", new { id = Class.InstructorID });
        }

        public async Task SaveCourse(Class myClass)
        {
            _context.Classes.Add(myClass);
            await _context.SaveChangesAsync();
        }
    }
}
