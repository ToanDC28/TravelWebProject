using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using TravelWebProject.service.BookingService;
using TravelWebProject.service.TourPlanServices;
using TravelWebProject.service.TourServices;

namespace TravelWebProject.web.Pages.BookingPage
{
    public class bookingDetailsModel : PageModel
    {
        private readonly IBookingService _context;
        private readonly ITourService _contextTour;
        public bookingDetailsModel()
        {
            _context = new BookingService();
            _contextTour = new TourService();
        }

      public Booking Booking { get; set; } = default!;
        public Tour Tour { get; set; }

        public async Task<IActionResult> OnGetAsync(int? bookingId)
        {
            if (bookingId != null)
                try
                { 
                    Booking = _context.GetById(bookingId.Value);
                    int tourID = Booking.TourId.Value;
                    if (tourID != null) {
                        Tour = _contextTour.GetTourById(tourID);
                    }
                }
                catch
                {
                    throw;
                }
           

            return Page();
        }
        }
    }

