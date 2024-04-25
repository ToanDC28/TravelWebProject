using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using TravelWebProject.service.TourServices;
using Microsoft.AspNetCore.Authorization;

namespace TravelWebProject.web.Pages.Tours
{
    [Authorize(Policy = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly ITourService _tourService;
        private readonly ILogger<DeleteModel> _logger;

        public DeleteModel(ITourService tourService, ILogger<DeleteModel> logger)
        {
            _tourService = tourService;
            _logger = logger;
        }

        [BindProperty]
        public Tour Tour { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            Tour = _tourService.GetTourById(id.Value);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                _logger.LogError("Tour ID is null");
                return NotFound();
            }

            Tour = _tourService.GetTourById(id.Value);
            _logger.LogInformation($"Tour ID: {Tour.TourId}");

            if (Tour != null)
            {
                _tourService.DeleteTour(Tour.TourId);
                _logger.LogInformation($"Tour {Tour.TourName} deleted");
            }

            return RedirectToPage("./Index");
        }
    }
}
