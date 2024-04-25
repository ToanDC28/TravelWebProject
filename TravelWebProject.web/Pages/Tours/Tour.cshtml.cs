using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TravelWebProject.service.TourServices;

namespace TravelWebProject.web.Pages.Tours
{
    public class TourModel : PageModel
    {
        private readonly ITourService _itourservice;

        public TourModel(ITourService itourservice)
        {
            _itourservice = itourservice;
        }

        public Tour Tour { get; set; }
        public List<Tour> Tours { get; set; } 
        //list tour


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            /*if (id == null)
            {
                return NotFound();
            }*/

            // G?i service ?? l?y thông tin tour theo TourId
           
            Tour = _itourservice.GetTourById(id.Value);
            Tours = _itourservice.GetTourByDestinationId(Tour.DestinateId);
            Tours.Remove(Tour);
            if (Tour == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
