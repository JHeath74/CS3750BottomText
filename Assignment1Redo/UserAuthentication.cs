using BottomTextLMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace BottomTextLMS
{

    [ApiController]
    public class UserAuthentication : ControllerBase
    {


        [Route("api/auth")]
        [HttpPost]
        public async Task<bool> PostAuth()
        {
            DbContextOptions<BottomTextLMS.AppDbContext> options = new DbContextOptions<BottomTextLMS.AppDbContext>();
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder(options);
            SqlServerDbContextOptionsExtensions.UseSqlServer(builder, "Data Source=titan.cs.weber.edu,10433;Initial Catalog=LMSGhln;User ID=LMSGhln;Password=Password*1", null);
            var _context = new BottomTextLMS.AppDbContext((DbContextOptions<BottomTextLMS.AppDbContext>)builder.Options);

            string requestData = string.Empty;
            using (StreamReader reader = new StreamReader(Request.Body, System.Text.Encoding.UTF8))
            {

                requestData = await reader.ReadToEndAsync();
            }

            User user = JsonConvert.DeserializeObject<User>(requestData);

            // TODO: Query db for user

            return true;
        }

    }
}
