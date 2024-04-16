using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace TravelWebProject.web.Pages
{
    [AllowAnonymous]
    public class LandingPage : PageModel
    {
        private readonly ILogger<LandingPage> _logger;

        public LandingPage(ILogger<LandingPage> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}