using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using TravelWebProject.service.TourServices;
using TravelWebProject.service.DestinationServices;

namespace TravelWebProject.web.Pages.Tours
{
    public class IndexModel : PageModel
    {
        private readonly ITourService _itourservice;
        private readonly IDestinationService _idesservice;

        public IndexModel()
        {
            _itourservice = new TourService();
            _idesservice = new  DestinationService();
        }

        public IList<Tour> Tour { get;set; } = default!;
        public IList<Destination> Destination { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            Tour = _itourservice.GetAllTours();
            return Page();
        }
    }
}
