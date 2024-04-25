using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;

namespace TravelWebProject.web.Pages.BookingPage
{
    public class bookingDetailsModel : PageModel
    {
        private readonly BusinessObject.Models.TravelWebContext _context;

        public bookingDetailsModel(BusinessObject.Models.TravelWebContext context)
        {
            _context = context;
        }

      public Booking Booking { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? bookingIdid)
        {
           
            return Page();
        }
    }
}
