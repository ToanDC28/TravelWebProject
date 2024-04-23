using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TravelWebProject.service.TourServices;

namespace TravelWebProject.web.Pages.Tours
{
    public class TourModel : PageModel
    {
        private readonly ITourService _itourservice;
        public TourModel()
        {
            _itourservice = new TourService();
        }
        public IList<Tour> Tour { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync()
        {
            Tour = _itourservice.GetAllTours();
            return Page();
        }
    }
}
