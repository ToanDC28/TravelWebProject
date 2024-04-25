using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using TravelWebProject.service.Users;

namespace TravelWebProject.web.Pages
{
    [AllowAnonymous]
    public class ResetPassword : PageModel
    {
        private readonly IDataProtectionProvider _dataProtectionProvider;
        private readonly IUserService _userService;
        private readonly ILogger<ResetPassword> _logger;

        public ResetPassword(IDataProtectionProvider dataProtectionProvider, IUserService userService, ILogger<ResetPassword> logger)
        {
            _dataProtectionProvider = dataProtectionProvider;
            _userService = userService;
            _logger = logger;
        }

        [BindProperty]
        [Required (ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
        [TempData]
        public string Message { get; set; }

        public void OnGet(string token)
        {
            if(User.Identity.IsAuthenticated)
            {
                Response.Redirect("/Index");
            }
        }

        public void OnPost()
        {
            var protector = _dataProtectionProvider.CreateProtector("ForgotPassword");
            var token = Request.Query["token"].ToString();
            var infor_token = protector.Unprotect(token);
            var tokenParts = infor_token.Split(':');
            var Email = tokenParts[0];

            if (ModelState.IsValid)
            {
                if (Password != ConfirmPassword)
                {
                    Message = "Passwords do not match.";
                    return;
                }
                // Update the password
                // Check if the user exists
                bool userExists = _userService.CheckUserExists(Email);
                if (!userExists)
                {
                    Message = "User does not exist.";
                    return;
                }
                // Update the password
                byte[] salt = new byte[128 / 8];
                var hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: Password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
                bool updated = _userService.UpdateUser(Email, hashedPassword);
                Message = "Password updated successfully.";
                System.Threading.Thread.Sleep(2000);
                Response.Redirect("/SignIn");
            }
        }
    }
}