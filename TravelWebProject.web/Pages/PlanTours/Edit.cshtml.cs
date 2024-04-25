using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;

namespace TravelWebProject.web.Pages.PlanTours
{
    public class EditModel : PageModel
    {
        private readonly BusinessObject.Models.TravelWebContext _context;

        public EditModel(BusinessObject.Models.TravelWebContext context)
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

            var tourplan =  await _context.TourPlans.FirstOrDefaultAsync(m => m.PlanId == id);
            if (tourplan == null)
            {
                return NotFound();
            }
            TourPlan = tourplan;
           ViewData["TourId"] = new SelectList(_context.Tours, "TourId", "Description");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(TourPlan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TourPlanExists(TourPlan.PlanId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool TourPlanExists(int id)
        {
          return (_context.TourPlans?.Any(e => e.PlanId == id)).GetValueOrDefault();
        }
    }
}
