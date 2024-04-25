using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TravelWebProject.repo.Users;
using TravelWebProject.service.BookingService;
using TravelWebProject.service.Users;
using System.Security.Claims;
using TravelWebProject.service.TourServices;

namespace TravelWebProject.web.Pages.BookingPage
{
    public class bookingFormModel : PageModel
    {
        private readonly IBookingService bookingService;
        private readonly IUserService userService;
        private readonly ITourService tourService;

        public bookingFormModel(IBookingService bookingService, IUserService userService, Booking booking)
        {
            this.bookingService = bookingService;
            this.userService = userService;
            tourService = new TourService();
        }
        [BindProperty]
        public Booking Booking { get; set; }
        public Tour Tour { get; set; }
        public IActionResult OnGet(int ? id)
        {
            Tour = tourService.GetTourById(id.Value);
            
            
            var user = HttpContext.User;
            if (user.Identity.IsAuthenticated)
            {
                var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim != null)
                {
                    // Lấy giá trị UserId
                    int userId;
                    if (int.TryParse(userIdClaim.Value, out userId))
                    {
                       User currentUser = bookingService.getUserFrombooking(userId);
                        Booking.User = currentUser;
                        return Page();

                    }
                    else
                    {
                        return Page();
                    }
                }
                else { return Page(); }
                }
            else
            {
              return  RedirectToPage("/SignIn");
            }
                   
        }

       
        public IActionResult OnPost()
        {
            if (Booking != null)
            {

                bookingService.Create(Booking);
                //return page LandingPage
                return RedirectToPage("./bookingDetails", new {bookingId = Booking.BookingId});
            }
            return Page();
        }

    }
}
