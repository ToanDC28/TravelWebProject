using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using TravelWebProject.service.TourServices;
using TravelWebProject.service.DestinationServices;
using TravelWebProject.service.TransportServices;

namespace TravelWebProject.web.Pages.Tours
{
    public class EditModel : PageModel
    {
        private readonly ITourService _tourService;
        private readonly IDestinationService _destinationService;
        private readonly ITransportService _transportService;
        private readonly ILogger<EditModel> _logger;
        public EditModel(ITourService tourService, IDestinationService destinationService, ITransportService transportService, ILogger<EditModel> logger)
        {
            _tourService = tourService;
            _destinationService = destinationService;
            _transportService = transportService;
            _logger = logger;
        }

        [BindProperty]
        public Tour Tour { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _tourService.GetAllTours() == null)
            {
                return NotFound();
            }

            var tour = _tourService.GetAllTours().FirstOrDefault(m => m.TourId == id);
            if (tour == null)
            {
                return NotFound();
            }
            Tour = tour;
            ViewData["DestinateId"] = new SelectList(_destinationService.GetDestinations(), "DestinationId", "Country");
            ViewData["TransportId"] = new SelectList(_transportService.GetAllTransportationModes(), "TransportationModeId", "TransportationModeId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            string[] dates = Tour.Duration.Split(" to ");
            Tour.StartDate = DateTime.Parse(dates[0]);
            Tour.EndDate = DateTime.Parse(dates[1]);
            //Update Duration to be the difference between StartDate and EndDate
            Tour.Duration = (Tour.EndDate - Tour.StartDate).Days.ToString();
            // if (!ModelState.IsValid)
            // {
            //     return Page();
            // }


            try
            {
                _tourService.UpdateTour(Tour);
                _logger.LogInformation($"Tour {Tour.TourName} updated successfully");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TourExists(Tour.TourId))
                {
                    _logger.LogError($"Tour {Tour.TourName} not found");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool TourExists(int id)
        {
            _logger.LogInformation($"Checking if tour with ID {id} exists");
            return _tourService.GetAllTours().Any(e => e.TourId == id);
        }
    }
}
