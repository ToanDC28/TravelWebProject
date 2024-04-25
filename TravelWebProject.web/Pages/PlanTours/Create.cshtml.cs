using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject.Models;
using TravelWebProject.service.DestinationServices;
using TravelWebProject.service.TourPlanServices;

namespace TravelWebProject.web.Pages.PlanTours
{
    public class CreateModel : PageModel
    {
        private readonly ITourPlanService _Service;

        public CreateModel()
        {
            _Service = new TourPlanService();
        }

        [BindProperty]
        public TourPlan TourPlan { get; set; } = default!;


        public IActionResult OnGet(int? id) { 
            if(id == null)
            {
                return NotFound();

            }
            TourPlan = new TourPlan
            {
                TourId = id.Value,
                // Khởi tạo các thuộc tính khác của TourPlan tại đây nếu cần thiết
            };
            // viewdata Tour
            ViewData["TourId"] = new SelectList(_Service.GetAllTourPlans(), "TourId", "TourId");

            return Page();      

        }


        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {   
             
            _Service.CreateTourPlan(TourPlan);

            return RedirectToPage("./Index");
        }
    }
}
