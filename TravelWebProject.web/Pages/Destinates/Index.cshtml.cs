using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;

namespace TravelWebProject.web.Pages.Destinates
{
    public class IndexModel : PageModel
    {
        private readonly BusinessObject.Models.TravelWebContext _context;

        public IndexModel(BusinessObject.Models.TravelWebContext context)
        {
            _context = context;
        }

        public IList<Destination> Destination { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Destinations != null)
            {
                Destination = await _context.Destinations
                .Include(d => d.Region).ToListAsync();
            }
        }
    }
}
