using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.CustomerServices;

namespace FUCarRentingSystem.Pages.CustomerPage
{
    public class LoginModel : PageModel
    {

        private readonly ICustomerService customerService;

        public LoginModel()
        {
            customerService = new CustomerService();
        }
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Password { get; set; }
        public IActionResult OnPost()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }
            var user = customerService.Login(Email, Password);
            if (user == null)
            {
                return Page();
            }
            return RedirectToPage("/Index");
        }
    }
}
