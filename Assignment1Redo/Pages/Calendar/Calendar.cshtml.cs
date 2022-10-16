using BottomTextLMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//var sourcearray = JSON.parse('@Html.Raw(Json.Serialize(Model.list_classes))');
//            var json = @Html.Raw(Json.Serialize(@Model.list_classes));

namespace BottomTextLMS.Pages.Calendar
{
    public class CalendarModel : PageModel
    {
        private readonly BottomTextLMS.AppDbContext _context;

        public CalendarModel(BottomTextLMS.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public User myUser { get; set; }

        public List<Class> list_classes { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            myUser = await _context.Users.FirstOrDefaultAsync(m => m.ID == id);

            if (myUser.Role == "Instructor")
            {
                var classesQuery = from row in _context.Classes where row.InstructorID == id select row;
                list_classes = classesQuery.ToList<Class>();
            }
            else if (myUser.Role == "Student")
            {
                var classesQuery = from enrollmentRow in _context.Enrollments
                                   join classRow in _context.Classes on enrollmentRow.ClassID equals classRow.ID
                                   where enrollmentRow.StudentID == id
                                   select classRow;

                list_classes = classesQuery.ToList<Class>();
            }

            return Page();
        }
    }
}
