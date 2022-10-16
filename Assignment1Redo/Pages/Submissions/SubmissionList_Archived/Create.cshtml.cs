﻿using BottomTextLMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace BottomTextLMS.Pages.Submissions
{
    public class AllSubmissionsCreateModel : PageModel
    {
        private readonly BottomTextLMS.AppDbContext _context;

        public AllSubmissionsCreateModel(BottomTextLMS.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["AssignmentID"] = new SelectList(_context.Assignments, "ID", "ID");
            return Page();
        }

        [BindProperty]
        public Submission Submission { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Submissions.Add(Submission);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
