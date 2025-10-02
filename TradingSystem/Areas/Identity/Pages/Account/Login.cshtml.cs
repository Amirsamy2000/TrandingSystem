// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using Castle.Core.Smtp;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrandingSystem.Domain.Entities;
using TrandingSystem.Domain.Helper;

namespace TrandingSystem.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly Microsoft.AspNetCore.Identity.UI.Services.IEmailSender _emailSender;



        public LoginModel(SignInManager<User> signInManager, UserManager<User> userManager, ILogger<LoginModel> logger, Microsoft.AspNetCore.Identity.UI.Services.IEmailSender emailSender)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string ErrorMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (!ModelState.IsValid)
                return Page();

            var user = await _userManager.FindByEmailAsync(Input.Email);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return Page();
            }

            // ✅ 1. Generate or read DeviceId from cookie
            var deviceId = DeviceHelper.GetOrCreateDeviceId(HttpContext);

            // ✅ 2. Compare DeviceId with what's stored in DB
            if (string.IsNullOrEmpty(user.DeviceId))
            {
                // أول مرة → اربط الجهاز باليوزر
                user.DeviceId = deviceId;
                await _userManager.UpdateAsync(user);
            }
            //else if (!_userManager.IsInRoleAsync(user, "Admin").Result && user.DeviceId != deviceId)
            //{
            //    // جهاز مختلف → امنع الدخول
            //    ModelState.AddModelError("", "You can only access your account from the first device you logged in on.");
            //    return Page();

            //}

            // ✅ 3. Continue normal login if device is correct


            if (user.IsBlocked)
            {
                ModelState.AddModelError(string.Empty, "Your account has been blocked. Contact support for help.");
                return Page();
            }

            

            // ✅ Check if email is confirmed
            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                var userId = await _userManager.GetUserIdAsync(user);
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                  "/Account/ConfirmEmail",
                  pageHandler: null,
                  values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                  protocol: Request.Scheme);

                await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    @$"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Account Confirmation</title>
    <style>
        * {{
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }}
        
        body {{
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            line-height: 1.6;
            color: #333;
            background-color: #f4f4f4;
            padding: 20px;
        }}
        
        .email-container {{
            max-width: 600px;
            margin: 0 auto;
            background-color: #ffffff;
            border-radius: 12px;
            overflow: hidden;
            box-shadow: 0 8px 32px rgba(20, 25, 33, 0.1);
        }}
        
        .header {{
            background: linear-gradient(135deg, #141921 0%, #1e2530 100%);
            padding: 40px 30px;
            text-align: center;
            position: relative;
        }}
        
        .header::before {{
            content: '';
            position: absolute;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background: url('data:image/svg+xml,<svg xmlns=""http://www.w3.org/2000/svg"" viewBox=""0 0 100 100""><defs><pattern id=""grid"" width=""10"" height=""10"" patternUnits=""userSpaceOnUse""><path d=""M 10 0 L 0 0 0 10"" fill=""none"" stroke=""%23c3972e"" stroke-width=""0.5"" opacity=""0.1""/></pattern></defs><rect width=""100"" height=""100"" fill=""url(%23grid)""/></svg>');
            opacity: 0.3;
        }}
        
        .logo-placeholder {{
            width: 80px;
            height: 80px;
            background: linear-gradient(45deg, #c3972e, #d4a944);
            border-radius: 50%;
            margin: 0 auto 20px;
            display: flex;
            align-items: center;
            justify-content: center;
            position: relative;
            z-index: 1;
        }}
        
        .logo-placeholder::before {{
            content: '✓';
            font-size: 32px;
            color: #141921;
            font-weight: bold;
        }}
        
        .header h1 {{
            color: #ffffff;
            font-size: 28px;
            font-weight: 600;
            margin-bottom: 8px;
            position: relative;
            z-index: 1;
        }}
        
        .header p {{
            color: rgba(255, 255, 255, 0.8);
            font-size: 16px;
            position: relative;
            z-index: 1;
        }}
        
        .content {{
            padding: 40px 30px;
            text-align: center;
        }}
        
        .welcome-text {{
            font-size: 18px;
            color: #141921;
            margin-bottom: 20px;
            font-weight: 500;
        }}
        
        .description {{
            font-size: 16px;
            color: #666;
            line-height: 1.8;
            margin-bottom: 35px;
            max-width: 480px;
            margin-left: auto;
            margin-right: auto;
        }}
        
        .confirm-button {{
            display: inline-block;
            background: linear-gradient(135deg, #c3972e 0%, #d4a944 100%);
            color: #ffffff;
            text-decoration: none;
            padding: 16px 40px;
            border-radius: 50px;
            font-size: 16px;
            font-weight: 600;
            letter-spacing: 0.5px;
            transition: all 0.3s ease;
            box-shadow: 0 4px 20px rgba(195, 151, 46, 0.3);
            margin-bottom: 25px;
        }}
        
        .confirm-button:hover {{
            transform: translateY(-2px);
            box-shadow: 0 6px 25px rgba(195, 151, 46, 0.4);
            background: linear-gradient(135deg, #d4a944 0%, #c3972e 100%);
        }}
        
        .security-note {{
            background: linear-gradient(135deg, rgba(195, 151, 46, 0.08), rgba(195, 151, 46, 0.12));
            border-left: 4px solid #c3972e;
            padding: 20px;
            margin: 30px 0;
            border-radius: 8px;
            text-align: left;
        }}
        
        .security-note h3 {{
            color: #141921;
            font-size: 16px;
            margin-bottom: 8px;
            font-weight: 600;
        }}
        
        .security-note p {{
            color: #666;
            font-size: 14px;
            line-height: 1.6;
        }}
        
        .footer {{
            background-color: #f8f9fa;
            padding: 25px 30px;
            text-align: center;
            border-top: 1px solid #e9ecef;
        }}
        
        .footer p {{
            color: #666;
            font-size: 14px;
            margin-bottom: 10px;
        }}
        
        .footer a {{
            color: #c3972e;
            text-decoration: none;
            font-weight: 500;
        }}
        
        .footer a:hover {{
            text-decoration: underline;
        }}
        
        .divider {{
            height: 2px;
            background: linear-gradient(90deg, transparent, #c3972e, transparent);
            margin: 20px auto;
            width: 60px;
        }}
        
        /* Responsive Design */
        @media (max-width: 600px) {{
            body {{
                padding: 10px;
            }}
            
            .header {{
                padding: 30px 20px;
            }}
            
            .content {{
                padding: 30px 20px;
            }}
            
            .header h1 {{
                font-size: 24px;
            }}
            
            .confirm-button {{
                padding: 14px 32px;
                font-size: 15px;
            }}
            
            .security-note {{
                margin: 20px 0;
                padding: 15px;
            }}
        }}
    </style>
</head>
<body>
    <div class=""email-container"">
        <!-- Header Section -->
        <div class=""header"">
            <div class=""logo-placeholder""></div>
            <h1>Account Confirmation</h1>
            <p>Welcome to our platform</p>
        </div>
        
        <!-- Main Content -->
        <div class=""content"">
            <div class=""welcome-text"">Almost there! Let's verify your account.</div>
            
            <div class=""description"">
                Thank you for joining us! To complete your registration and secure your account,the button below to confirm your email address.
            </div>
            
            <a href=""{callbackUrl}"" class=""confirm-button"">
                Confirm My Account
            </a>
            
            <div class=""divider""></div>
            
            <div class=""security-note"">
                <h3>🔒 Security Notice</h3>
                <p>This confirmation link will expire in 24 hours for your security. If you didn't create an account with us, please ignore this email or contact our support team.</p>
            </div>
        </div>
        
        <!-- Footer Section -->
        <div class=""footer"">
            <p>Need help? <a href=""#"">Contact our support team</a></p>
            <p>© 2025 Your Company Name. All rights reserved.</p>
        </div>
    </div>
</body>
</html>");


                ModelState.AddModelError(string.Empty, "Your email is not confirmed. Please check your inbox.");
                return Page();
            }

            // Try sign in (with lockout on failure)
            var result = await _signInManager.PasswordSignInAsync(
                Input.Email,
                Input.Password,
                Input.RememberMe,
                lockoutOnFailure: true);

            if (result.Succeeded)
            {
                // 🔐 Invalidate other sessions
                await _userManager.UpdateSecurityStampAsync(user);

                // 🔐 Sign in again
                await _signInManager.SignInAsync(user, Input.RememberMe);

                _logger.LogInformation("User logged in with single session.");
                return LocalRedirect(returnUrl);
            }

            if (result.RequiresTwoFactor)
            {
                return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
            }

            if (result.IsLockedOut)
            {
                _logger.LogWarning("User account locked out.");
                return RedirectToAction("LockedAccount", "Home");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return Page();
        }

    }
}
