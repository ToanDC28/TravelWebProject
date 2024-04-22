using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;

namespace TravelWebProject.web.Pages.Tours
{
    public class DetailsModel : PageModel
    {
        private readonly BusinessObject.Models.TravelWebContext _context;

        public DetailsModel(BusinessObject.Models.TravelWebContext context)
        {
            _context = context;
        }

      public Tour Tour { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Tours == null)
            {
                return NotFound();
            }

            var tour = await _context.Tours.FirstOrDefaultAsync(m => m.TourId == id);
            if (tour == null)
            {
                return NotFound();
            }
            else 
            {
                Tour = tour;
            }
            return Page();
        }
    }
}
