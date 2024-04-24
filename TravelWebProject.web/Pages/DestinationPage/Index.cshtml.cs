using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TravelWebProject.service.DestinationServices;

namespace TravelWebProject.web.Pages.DestinationPage
{
    public class IndexModel : PageModel
    {

        private readonly IDestinationService destinationService;

        public IndexModel()
        {
            this.destinationService = new DestinationService();
        }
        public List<Destination> Destinations { get; set; }
        public void OnGet()
        {
            Destinations = destinationService.GetDestinations();
        }
    }
}
