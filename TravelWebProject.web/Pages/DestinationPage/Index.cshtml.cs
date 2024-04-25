using BusinessObject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TravelWebProject.service.DestinationServices;

namespace TravelWebProject.web.Pages.DestinationPage
{
    [Authorize(Policy = "AdminAndCustomer")]
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
