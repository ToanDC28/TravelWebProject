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

        public bookingFormModel(IBookingService bookingService,  ITourService tourService, IUserService userService, Booking booking)
        {
            this.bookingService = bookingService;
            this.userService = userService;
            this.tourService = tourService;
        }
        [BindProperty]
        public Booking Booking { get; set; }
        [BindProperty]
        public int amountOfPeople {  get; set; }
        [BindProperty]
        public Tour Tour { get; set; }
        [BindProperty]
        public User User { get; set; }

        public IActionResult OnGet(int ? id)
        {
            Tour = tourService.GetTourById(id.Value);
            if (Tour != null)
            {


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
                            User = bookingService.getUserFrombooking(userId);
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
                    return RedirectToPage("/SignIn");
                }
            }else {
                return RedirectToPage("/LandingPage");
            }
                   
        }

       
        public IActionResult OnPost()
        {
            Booking booking = new Booking();
            var tour = tourService.GetTourById(Tour.TourId);
            booking.UserId = User.UserId;
            booking.TourId = tour.TourId;
            booking.Status = "ACTIVE";
            booking.AmountOfPeople = amountOfPeople;
            booking.TotalAmount = Tour.TotalCost * amountOfPeople;
            booking.RemainingAmount = booking.TotalAmount;
            booking.BookingDate = DateTime.Now;
            booking.PaidAmount = 0;
            booking.PaymentDeadline = tour.StartDate;
             bookingService.Create(booking);
                //return page LandingPage
                return RedirectToPage("/BookingPage/bookingDetails", new { bookingId = booking.BookingId });

        }

    }
}
