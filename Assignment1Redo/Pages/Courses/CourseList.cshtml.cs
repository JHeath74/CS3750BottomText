using BottomTextLMS.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BottomTextLMS.Pages.Courses
{
    public class IndexModel : PageModel
    {
        private readonly BottomTextLMS.AppDbContext _context;

        public IndexModel(BottomTextLMS.AppDbContext context)
        {
            _context = context;
        }

        public User myProf { get; set; }

        public List<Class> Class { get; set; }

        public async Task OnGetAsync(int? id)
        {
            myProf = await _context.Users.FirstOrDefaultAsync(m => m.ID == id);

            Class = await _context.Classes.Where(m => m.InstructorID == myProf.ID).ToListAsync();

            if (Class.Count() > 0)
            {
                var roomQuery = from r in _context.Rooms
                                where r.ID == Class[0].RoomID
                                select r;
                Class[0].Room = await roomQuery.SingleAsync();
            }
        }
    }
}