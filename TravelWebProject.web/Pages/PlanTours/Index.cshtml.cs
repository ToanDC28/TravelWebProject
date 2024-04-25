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
using Microsoft.AspNetCore.Authorization;

namespace TravelWebProject.web.Pages.PlanTours
{
    [Authorize(Policy = "AdminAndCustomer")]
    public class IndexModel : PageModel
    {
        private readonly ITourPlanService _itourservice;

        public IndexModel()
        {
            _itourservice = new TourPlanService();
        }

        public IList<TourPlan> TourPlan { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            TourPlan = _itourservice.GetAllTourPlans();
            return Page();
        }
    }
}
