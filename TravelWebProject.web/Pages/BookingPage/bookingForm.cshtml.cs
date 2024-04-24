using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TravelWebProject.repo.Users;
using TravelWebProject.service.BookingService;
using TravelWebProject.service.Users;

namespace TravelWebProject.web.Pages.BookingPage
{
    public class bookingFormModel : PageModel
    {
        private readonly IBookingService bookingService;
        private readonly IUserService userService;

        public bookingFormModel(IBookingService bookingService, IUserService userService, Booking booking)
        {
            this.bookingService = bookingService;
            this.userService = userService;
        }
        public User User { get; set; }
        public void OnGet( )
        {
            // đưa cookie vào để lấy mail
            
             string mail = "";

            User = userService.GetUser(mail);          
        }

        public Booking Booking { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            if (Booking != null)
            {

                bookingService.Create(Booking);
                return RedirectToPage("/booking");
            }
            return Page();
        }

    }
}
