using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using TravelWebProject.service.TourPlanServices;
using TravelWebProject.service.TourServices;

namespace TravelWebProject.web.Pages.PlanTours
{
    public class DetailsModel : PageModel
    {
        private readonly ITourPlanService tourPlanService;

        public DetailsModel()
        {
            tourPlanService = new TourPlanService();
        }

        public IList<TourPlan> TourPlans { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else { 
            TourPlans = tourPlanService.GetTourPlansByTourId(id.Value);
            return Page();
            }
        }
    }
}
