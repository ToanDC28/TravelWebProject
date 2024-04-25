using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using Microsoft.AspNetCore.Authorization;

namespace TravelWebProject.web.Pages.Destinates
{
    [Authorize(Policy = "Admin, Customer")]
    public class DetailsModel : PageModel
    {
        private readonly BusinessObject.Models.TravelWebContext _context;

        public DetailsModel(BusinessObject.Models.TravelWebContext context)
        {
            _context = context;
        }

      public Destination Destination { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Destinations == null)
            {
                return NotFound();
            }

            var destination = await _context.Destinations.FirstOrDefaultAsync(m => m.DestinationId == id);
            if (destination == null)
            {
                return NotFound();
            }
            else 
            {
                Destination = destination;
            }
            return Page();
        }
    }
}
