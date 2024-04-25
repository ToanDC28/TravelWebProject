using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject.Models;

namespace TravelWebProject.web.Pages.Destinates
{
    public class CreateModel : PageModel
    {
        private readonly BusinessObject.Models.TravelWebContext _context;

        public CreateModel(BusinessObject.Models.TravelWebContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["RegionId"] = new SelectList(_context.Regions, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Destination Destination { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Destinations == null || Destination == null)
            {
                return Page();
            }

            _context.Destinations.Add(Destination);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
