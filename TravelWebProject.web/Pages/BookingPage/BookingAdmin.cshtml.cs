using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using TravelWebProject.service.BookingService;

namespace TravelWebProject.web.Pages.BookingPage
{
    public class BookingAdminModel : PageModel
    {
        private readonly IBookingService _context;

        public BookingAdminModel()
        {
            _context = new BookingService();
        }

        public IList<Booking> Booking { get; set; } = default!;

        public IActionResult OnGetAsync()
        {
            var user = HttpContext.User;
            if (user.Identity.IsAuthenticated)
            {
                var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);
                int userId;
                if (int.TryParse(userIdClaim.Value, out userId))
                {

                    Booking = _context.GetAll();
                    if (Booking == null)
                    {
                        Booking = new List<Booking>();

                        return NotFound();
                    }
                    else
                    {
                        return Page();
                    }
                }
                else
                {
                    return RedirectToPage("/SignIn");

                }
            }
            else
            {
                return RedirectToPage("/SignIn");
            }
        }
    }

}
