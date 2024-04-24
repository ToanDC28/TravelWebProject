using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using TravelWebProject.service.DestinationServices;
using TravelWebProject.service.RegionService;
using TravelWebProject.service.RegionServices;
using TravelWebProject.service.TourServices;
using TravelWebProject.service.TransportServices;

namespace TravelWebProject.web.Pages
{
    [AllowAnonymous]
    public class LandingPage : PageModel
    {
        private readonly ILogger<LandingPage> _logger;
        private readonly IRegionService regionService;
        private readonly ITransportService transportService;
        private readonly ITourService tourService;
        private readonly IDestinationService destinationService;
        public List<Destination> Destinations { get; set; }
        public List<Tour> Tours { get; set; }
        public string whereTo {  get; set; }
        public string date {  get; set; }
        public string tourType {  get; set; }
        public LandingPage(ILogger<LandingPage> logger)
        {
            _logger = logger;
            regionService = new RegionService();
            transportService = new TransportService();
            destinationService = new DestinationService();
            tourService = new TourService();
        }

        public void OnGet()
        {
            ViewData["Name"] = new SelectList(regionService.GetAll(), "Id", "Name");
            ViewData["TransportationModeId"] = new SelectList(transportService.GetAllTransportationModes(), "TransportationModeId", "Mode");
            Destinations = destinationService.GetDestinations().Take(4).ToList();
            Tours = tourService.GetAllTours().Take(3).ToList();
        }
        /*public IActionResult OnPost()
        {

        }*/
    }
}