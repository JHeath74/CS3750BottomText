using BottomTextLMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BottomTextLMS.Pages.StudentAccount
{
    public class CreateModel : PageModel
    {
        private readonly BottomTextLMS.AppDbContext _context;

        public CreateModel(BottomTextLMS.AppDbContext context)
        {
            _context = context;
        }

        public User Student { get; set; }

        public List<Enrollment> Enrollments { get; set; }

        public List<Class> Classes { get; set; }

        public int studentCreditHours { get; set; }

        public int studentMoneyOwed { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, Boolean payed)
        {
            Student = await _context.Users.FirstOrDefaultAsync(m => m.ID == id);

            Enrollments = await _context.Enrollments.Where(m => m.StudentID == Student.ID).ToListAsync();

            Classes = await _context.Classes.ToListAsync();

            foreach (Enrollment enroll in Enrollments)
            {
                foreach (Class studentClass in Classes)
                {
                    if (enroll.StudentID == Student.ID && enroll.ClassID == studentClass.ID)
                    {
                        studentCreditHours += studentClass.CreditHours;
                    }
                }
            }

            if (payed)
            {
                studentMoneyOwed = 0;
            }
            else
            {
                studentMoneyOwed = studentCreditHours * 100;
            }

            return Page();
        }

        [BindProperty]
        public CreditCard CreditCard { get; set; }
        [BindProperty]
        public int studentID { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Use stripe API to charge credit card

            // Http client to make requests
            HttpClient client = new HttpClient();

            // -------------TOKEN REQUEST-------------

            // Content body for token request
            var tokenContent = new FormUrlEncodedContent(new Dictionary<string, string> { { "card[number]", "4242424242424242" },
                { "card[exp_month]", CreditCard.ExpDate.Month.ToString()}, { "card[exp_year]", CreditCard.ExpDate.Year.ToString() },
                { "card[cvc]", CreditCard.CVC.ToString() } , { "card[name]", CreditCard.HolderName } });

            // Configure request message
            HttpRequestMessage tokenMessage = new HttpRequestMessage(HttpMethod.Post, "https://api.stripe.com/v1/tokens");
            tokenMessage.Headers.Add("authorization", "Bearer sk_test_51JgtvFBbu9ealfPFexPeQcrGh7ByHnSd7sMoQC6vpsrfwLyzGKBUHTZn9RUjz6ViIV65tL6xDsCUMdk2JitE0CQw00nMu6GfWg");
            tokenMessage.Content = tokenContent;

            // Send request message and process response
            var tokenResponse = await client.SendAsync(tokenMessage);

            string tokenJsonString = await tokenResponse.Content.ReadAsStringAsync();
            var tokenID = (string)JsonConvert.DeserializeObject<Dictionary<string, object>>(tokenJsonString)["id"];

            // ---------------------------------------



            // -------------CHARGE REQUEST-------------

            // Content body for charge request
            var chargeContent = new FormUrlEncodedContent(new Dictionary<string, string> { { "amount", CreditCard.AmountToPay.ToString() + "00" },
                { "currency", "usd" }, { "source", tokenID }, { "description", "Tuition Payment" } });

            // Configure request message
            HttpRequestMessage chargeMessage = new HttpRequestMessage(HttpMethod.Post, "https://api.stripe.com/v1/charges");
            chargeMessage.Headers.Add("authorization", "Bearer sk_test_51JgtvFBbu9ealfPFexPeQcrGh7ByHnSd7sMoQC6vpsrfwLyzGKBUHTZn9RUjz6ViIV65tL6xDsCUMdk2JitE0CQw00nMu6GfWg");
            chargeMessage.Content = chargeContent;

            // Send request message and process response
            var chargeResponse = await client.SendAsync(chargeMessage);

            string chargeJsonString = await chargeResponse.Content.ReadAsStringAsync();

            // ---------------------------------------
<<<<<<< Updated upstream
            
            return RedirectToPage( "./StudentAccount", new { id = studentID, payed = true });
=======

            return RedirectToPage("./StudentAccount", new { id = studentID });
>>>>>>> Stashed changes
        }
    }
}
