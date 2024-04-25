using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using TravelWebProject.service.BookingService;
using System.Security.Claims;

namespace TravelWebProject.web.Pages.BookingPage
{
    public class bookingIndexModel : PageModel
    {
        private readonly IBookingService _context;

        public bookingIndexModel()
        {
            _context = new BookingService();
        }
        
        public IList<Booking> Booking { get; set; } = default!;
        
        public async Task OnGetAsync()
        {
            var user = HttpContext.User;
            if (user.Identity.IsAuthenticated)
            {
                var userIdClaim = user.FindFirst(ClaimTypes.Name);
                int userId;
                if (int.TryParse(userIdClaim.Value, out userId))
                {

                    Booking = _context.GetAllByUser(userId);
                    if(Booking == null)
                    {
                        Booking = new List<Booking>();


                    }


                }
            }
        }
    }
}
