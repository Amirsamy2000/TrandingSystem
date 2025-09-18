// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TrandingSystem.Domain.Entities;

namespace TrandingSystem.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IUserStore<User> _userStore;
        private readonly IUserEmailStore<User> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
       

        public RegisterModel(
            UserManager<User> userManager,
            IUserStore<User> userStore,
            SignInManager<User> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
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
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

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
            [Display(Name = "Full Name")]
            public string FullName { get; set; }

            [Required]
            [Display(Name = "Mobile")]
            public string Mobile { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }


            [Required]
            [DataType(DataType.PostalCode)]
            [MaxLength(14)]
            [Display(Name = "National ID")]
            public string NationalId { get; set; }


            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                var user = CreateUser();
                user.FullName = Input.FullName;
                user.Mobile = Input.Mobile;
                user.NationalId = Input.NationalId;

                var dbContext = HttpContext.RequestServices
                    .GetService(typeof(TrandingSystem.Infrastructure.Data.db23617Context))
                    as TrandingSystem.Infrastructure.Data.db23617Context;

                // ✅ Check duplicate NationalId
                var existingUser = await dbContext.Users.FirstOrDefaultAsync(u => u.NationalId == Input.NationalId);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Input.NationalId", "This National ID is already registered.");
                    return Page();
                }

                // ✅ Create user
                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);

                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    // Assign default role
                    if (!await _userManager.IsInRoleAsync(user, "User"))
                    {
                        await _userManager.AddToRoleAsync(user, "User");
                    }

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Hom/Index",
                        pageHandler: null,
                        values:null,
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
                Thank you for joining us! To complete your registration and secure your account, please click the button below to confirm your email address.
            </div>
            
            <a href=""{HtmlEncoder.Default.Encode(callbackUrl)}"" class=""confirm-button"">
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

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("ConfirmEmail", new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private User CreateUser()
        {
            try
            {
                return Activator.CreateInstance<User>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(User)}'. " +
                    $"Ensure that '{nameof(User)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<User> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<User>)_userStore;
        }
    }
}
