using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject.Models;

namespace TravelWebProject.web.Pages.BookingPage
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
        ViewData["TourId"] = new SelectList(_context.Tours, "TourId", "Description");
        ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId");
            return Page();
        }

        [BindProperty]
        public Booking Booking { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Bookings == null || Booking == null)
            {
                return Page();
            }

            _context.Bookings.Add(Booking);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
