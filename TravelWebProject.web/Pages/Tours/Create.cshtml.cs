using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject.Models;

namespace TravelWebProject.web.Pages.Tours
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
        ViewData["DestinateId"] = new SelectList(_context.Destinations, "DestinationId", "Country");
        ViewData["TransportId"] = new SelectList(_context.TransportationModes, "TransportationModeId", "TransportationModeId");
            return Page();
        }

        [BindProperty]
        public Tour Tour { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Tours == null || Tour == null)
            {
                return Page();
            }

            _context.Tours.Add(Tour);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
