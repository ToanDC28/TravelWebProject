using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TravelWebProject.web.Pages.DestinationPage
{
    public class IndexModel : PageModel
    {
<<<<<<< Updated upstream
=======

        private readonly IDestinationService destinationService;
  
        public IndexModel()
        {
            this.destinationService = new DestinationService();
        }
        public List<Destination> Destinations { get; set; }
>>>>>>> Stashed changes
        public void OnGet()
        {
        }
    }
}
