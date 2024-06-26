using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using TravelWebProject.service.Authentication;

namespace TravelWebProject.web.Pages
{
    [AllowAnonymous]
    public class SignIn : PageModel
    {
        private readonly ICustomAuthenticationService _authenticationService;
        private readonly ILogger<SignIn> _logger;

        public SignIn(ILogger<SignIn> logger, ICustomAuthenticationService authenticationService)
        {
            _logger = logger;
            _authenticationService = authenticationService;
        }

        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Password { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (User.Identity.IsAuthenticated)
            { 
                return RedirectToPage("/LandingPage");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            byte[] salt = new byte[128 / 8];
            var hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: Password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            var user = _authenticationService.Authenticate(Email, hashedPassword);
            if (user != null)
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Role, user.Role.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())

            };
                var claimsIdentity = new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,

                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
                };
                await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme, 
            new ClaimsPrincipal(claimsIdentity), 
            authProperties);
                //Ghi log đăng nhập thành công
                _logger.LogInformation("User {Email} logged in at {Time}.", Email, DateTime.UtcNow);
                return RedirectToPage("/LandingPage");
            } else {
                //Ghi log lỗi
                _logger.LogError("User {Email} failed to log in at {Time}.", Email, DateTime.UtcNow);
                TempData["Error"] = "Email hoặc mật khẩu không đúng";
            }

            // Xử lý việc đăng nhập thất bại
            return Page();
        }
    }
}