using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.Models;
using Microsoft.AspNetCore.Authorization;
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

        public void OnGet()
        {
        }



        public async Task<IActionResult> OnPostRegisterUserAsync([FromForm] RegisterFormData formData)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(new { success = false, message = "Invalid form data." });
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

                var new_user = new User
                {
                    FullName = formData.Name,
                    Username = formData.Username,
                    Email = formData.Email,
                    Phone = formData.PhoneNumber,
                    Password = formData.Password
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