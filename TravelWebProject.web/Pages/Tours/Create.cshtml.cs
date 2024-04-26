using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject.Models;
using TravelWebProject.repo.TourPlans;
using TravelWebProject.service.TourPlanServices;
using TravelWebProject.service.TourServices;
using TravelWebProject.service.DestinationServices;
using TravelWebProject.service.TransportServices;
using Microsoft.AspNetCore.Authorization;
using System.Globalization;

namespace TravelWebProject.web.Pages.Tours
{
    [Authorize(Policy = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly ITourService _tourService;
        private readonly IDestinationService _destinationService;
        private readonly ITransportService _transportService;

        public CreateModel()
        {
            _tourService = new TourService();
            _destinationService = new DestinationService();
            _transportService = new TransportService();
        }

        public IActionResult OnGet()
        {
            Tour = new Tour
            {
                TotalRating = "0"
            };
           
            ViewData["DestinateId"] = new SelectList(_destinationService.GetDestinations(), "DestinationId", "Country");
        ViewData["TransportId"] = new SelectList(_transportService.GetAllTransportationModes(), "TransportationModeId", "Mode");
            return Page();
        }

        [BindProperty]
        public Tour Tour { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            string[] dates = Tour.Duration.Split(" to ");
            DateTime startDate;
            DateTime endDate;

            if (dates.Length == 2 && DateTime.TryParseExact(dates[0], "MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate) &&
                DateTime.TryParseExact(dates[1], "MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate))
            {
                Tour.StartDate = startDate;
                Tour.EndDate = endDate;

                Tour.Duration = (Tour.EndDate - Tour.StartDate).TotalDays.ToString();
            }
            else
            {
                Tour.StartDate = DateTime.Now; 
                Tour.EndDate = DateTime.Now;
                Tour.Duration = "0"; 
            }

            Tour.TotalRating = "0";
            _tourService.AddTour(Tour);

            return RedirectToPage("./Index");
        }
    }
}
