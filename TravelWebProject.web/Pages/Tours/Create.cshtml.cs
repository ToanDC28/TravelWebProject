using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject.Models;
using TravelWebProject.repo.TourPlans;
using TravelWebProject.service.TourPlanServices;
using TravelWebProject.service.TourServices;
using TravelWebProject.service.DestinationServices;
using TravelWebProject.service.TransportServices;

namespace TravelWebProject.web.Pages.Tours
{
    public class CreateModel : PageModel
    {
        private readonly ITourService _tourService;
        private readonly IDestinationService _destinationService;
        private readonly ITransportService _transportService;

        public CreateModel()
        {
            _tourService = new TourService();
            _destinationService = new DestinationService();
            _transportService = new TransportService();
        }

        public IActionResult OnGet()
        {
/*            if (HttpContext.Session.GetString("role") == null)
            {
                return RedirectToPage("/CustomerPage/Login");

            }
            string role = HttpContext.Session.GetString("role");
            if (!role.Equals("admin"))
            {
                return RedirectToPage("/CustomerPage/Login");
            }*/
        ViewData["DestinateId"] = new SelectList(_destinationService.GetDestinations(), "DestinationId", "Country");
        ViewData["TransportId"] = new SelectList(_transportService.GetAllTransportationModes(), "TransportationModeId", "TransportationModeId");
            return Page();
        }

        [BindProperty]
        public Tour Tour { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
           // Duration: 04-16 to 04-26 split by "to" and convert to DateTime
            string[] dates = Tour.Duration.Split(" to ");
            Tour.StartDate = DateTime.Parse(dates[0]);
            Tour.EndDate = DateTime.Parse(dates[1]);
            //Update Duration to be the difference between StartDate and EndDate
            Tour.Duration = (Tour.EndDate - Tour.StartDate).Days.ToString();
        //    if (!ModelState.IsValid || Tour == null)
        //     {
        //         return Page();
        //     }
            

            _tourService.AddTour(Tour);

            return RedirectToPage("./Index");
        }
    }
}
