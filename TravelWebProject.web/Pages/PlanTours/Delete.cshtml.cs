using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;

namespace TravelWebProject.web.Pages.PlanTours
{
    public class DeleteModel : PageModel
    {
        private readonly BusinessObject.Models.TravelWebContext _context;

        public DeleteModel(BusinessObject.Models.TravelWebContext context)
        {
            _context = context;
        }

        [BindProperty]
      public TourPlan TourPlan { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.TourPlans == null)
            {
                return NotFound();
            }

            var tourplan = await _context.TourPlans.FirstOrDefaultAsync(m => m.PlanId == id);

            if (tourplan == null)
            {
                return NotFound();
            }
            else 
            {
                TourPlan = tourplan;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.TourPlans == null)
            {
                return NotFound();
            }
            var tourplan = await _context.TourPlans.FindAsync(id);

            if (tourplan != null)
            {
                TourPlan = tourplan;
                _context.TourPlans.Remove(TourPlan);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
