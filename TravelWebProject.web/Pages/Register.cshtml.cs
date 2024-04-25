using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using BusinessObject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using TravelWebProject.service.Users;
using TravelWebProject.web.ViewModels;

namespace TravelWebProject.web.Pages
{
    [AllowAnonymous]
    public class Register : PageModel
    {
        private readonly ILogger<IUserService> _logger;
        private readonly IUserService _userService;

        public Register(ILogger<IUserService> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/LandingPage");
            }

            return Page();
        }



        public async Task<IActionResult> OnPostRegisterUserAsync([FromForm] RegisterFormData formData)
        {
            if (!ModelState.IsValid)
            {
                string errors = string.Join(", ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                return new JsonResult(new { success = false, message = errors });
            }

            try
            {
                var userExists = _userService.GetUser(formData.Email);

                if (formData.Password != formData.PasswordRepeat)
                {
                    return new JsonResult(new { success = false, message = "Passwords do not match." });
                }

                if (userExists != null)
                {
                    return new JsonResult(new { success = false, message = "User with this email already exists." });
                }

                byte[] salt = new byte[128 / 8];

                var new_user = new User
                {
                    FullName = formData.Name,
                    Username = formData.Username,
                    Email = formData.Email,
                    Phone = formData.PhoneNumber,
                    Password = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                        password: formData.Password,
                        salt: salt,
                        prf: KeyDerivationPrf.HMACSHA1,
                        iterationCount: 10000,
                        numBytesRequested: 256 / 8)),
                    RoleId = 2
                };
                var result = _userService.RegisterUser(new_user);
                if (result)
                {
                    _logger.LogInformation("User registration successful for: " + formData.Email);
                    return new JsonResult(new { success = true, message = "Registration successful" });
                }
                else
                {
                    _logger.LogWarning("User registration failed for: " + formData.Email);
                    return new JsonResult(new { success = false, message = "Registration failed, please check your data." });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occured while registering user: " + formData.Email);
                return new JsonResult(new { success = false, message = ex.Message });
            }
        }
    }
}