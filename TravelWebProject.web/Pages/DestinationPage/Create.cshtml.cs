using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject.Models;
using TravelWebProject.service.DestinationServices;
using TravelWebProject.service.TourServices;
using TravelWebProject.service.TransportServices;
using TravelWebProject.service.RegionService;
using TravelWebProject.service.RegionServices;

namespace TravelWebProject.web.Pages.DestinationPage
{
    public class CreateModel : PageModel
    {
        private readonly IRegionService _regionService;
        private readonly IDestinationService _destinationService;


        public CreateModel()
        {
            _regionService = new RegionService();
            _destinationService = new DestinationService();
        }

        public IActionResult OnGet()
        {
        ViewData["RegionId"] = new SelectList(_regionService.GetAll(), "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Destination Destination { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            _destinationService.AddDestination(Destination);

            return RedirectToPage("./Index");
        }
    }
}
