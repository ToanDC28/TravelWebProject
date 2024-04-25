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

namespace TravelWebProject.web.Pages.Destinates
{
    public class IndexModel : PageModel
    {
        private readonly IDestinationService _idesservice;

        public IndexModel()
        {
            _idesservice = new  DestinationService();
        }

        public IList<Destination> Destination { get;set; } = default!;

        public ActionResult OnGetAsync()
        {
            Destination = _idesservice.GetDestinations();
            return Page();
            
        }
    }
}
