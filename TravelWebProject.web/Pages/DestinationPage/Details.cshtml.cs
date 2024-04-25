using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using TravelWebProject.service.TourServices;
using TravelWebProject.service.DestinationServices;
using Microsoft.AspNetCore.Authorization;

namespace TravelWebProject.web.Pages.DestinationPage
{
    [Authorize(Policy = "AdminAndCustomer")]
    public class DetailsModel : PageModel
    {
        private readonly IDestinationService destinationService;
        private readonly ITourService tourService;

        public DetailsModel()
        {
            destinationService = new DestinationService();
            tourService = new TourService();
        }

      public Destination Destination { get; set; } = default!;
        public List<Tour> Tours { get; set; } = default;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Destination = destinationService.GetDestination(id.Value);
            Tours = tourService.GetTourByDestinationId(id.Value);
            return Page();
        }
    }
}
