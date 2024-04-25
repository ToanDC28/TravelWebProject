using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.Models;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using TravelWebProject.service.Mail;
using TravelWebProject.service.Users;


namespace TravelWebProject.web.Pages
{
    public class ForgotPassword : PageModel
    {
        private readonly IDataProtectionProvider _dataProtectionProvider;
        private readonly IMailService _emailSender;
        private readonly IUserService _userService;
        private const int TokenLifespan = 5;
        private readonly ILogger<ForgotPassword> _logger;
        [BindProperty]
        public string Email { get; set; }
        [TempData]
        public string Message { get; set; }

        public ForgotPassword(IDataProtectionProvider dataProtectionProvider, IMailService emailSender, IUserService userService, ILogger<ForgotPassword> logger)
        {
            _dataProtectionProvider = dataProtectionProvider;
            _emailSender = emailSender;
            _userService = userService;
            _logger = logger;
        }

        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                // Check if the user exists
                bool userExists = _userService.CheckUserExists(Email);
                if (!userExists)
                {
                    Message = "User does not exist.";
                    return Page();
                }
                var protector = _dataProtectionProvider.CreateProtector("ForgotPassword");
                //Lưu thông tin email và thời gian hết hạn cũng như thời gian hiện tại
                var infor = $"{Email}:{DateTimeOffset.UtcNow}:{DateTimeOffset.UtcNow.AddMinutes(TokenLifespan)}";
                var token = protector.Protect(infor);
                var mailData = new MailData
                {
                    EmailToId = Email,
                    EmailToName = "User",
                    EmailSubject = "Reset Password",
                    EmailBody = $"Click <a href='https://localhost:7285/ResetPassword?token={token}'>here</a> to reset your password. The link will expire in {TokenLifespan} minutes."
                };

                if (_emailSender.SendMail(mailData))
                {
                    Message = "An email has been sent to your email address. Please check your email to reset your password.";
                }
                else
                {
                    Message = "An error occurred while sending the email. Please try again later.";
                }
            }
            return Page();
        }
    }
}