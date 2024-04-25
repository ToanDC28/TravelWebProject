using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using TravelWebProject.service.TourServices;
using TravelWebProject.service.TourPlanServices;

namespace TravelWebProject.web.Pages.PlanTours
{
    public class DeleteModel : PageModel
    {
        private readonly ITourPlanService _tourService;
        private readonly ILogger<DeleteModel> _logger;

        public DeleteModel(ITourPlanService tourService, ILogger<DeleteModel> logger)
        {
            _tourService = tourService;
            _logger = logger;
        }

        [BindProperty]
      public TourPlan TourPlan { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            TourPlan = _tourService.GetTourPlanById(id.Value);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                _logger.LogError("Tourplan ID is null");
                return NotFound();
            }

            TourPlan = _tourService.GetTourPlanById(id.Value);
            _logger.LogInformation($"TourPlan ID: {TourPlan.PlanId}");

            if (TourPlan != null)
            {
                _tourService.DeleteTourPlan(TourPlan.PlanId);
                _logger.LogInformation($"Tour {TourPlan.Name} deleted");
            }

            return RedirectToPage("./Index");
        }
    }
}
