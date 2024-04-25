using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TravelWebProject.service.BookingService;

namespace TravelWebProject.web.Pages.BookingPage
{
    public class PaymentModel : PageModel
    {
        private readonly IBookingService _context;

        public PaymentModel()
        {
            _context = new BookingService();
        }
        public IActionResult OnGet(int?id)
        {
            try
            {
                Id = id.Value;

                Booking = _context.GetBookingFullInfor(Id);
            }
            catch
            {
                throw;
            }
            return Page();
        }
        public int Id { get; set; }
        public Booking Booking { get; set; } = default!;
    }


}
