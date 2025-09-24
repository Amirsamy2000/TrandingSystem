// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using TrandingSystem.Domain.Entities;

namespace TrandingSystem.Areas.Identity.Pages.Account
{
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<User> _userManager;

        public ConfirmEmailModel(UserManager<User>   userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }
        public int Status { get; set; }
        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            string culture = HttpContext.Features.Get<IRequestCultureFeature>()
                        .RequestCulture.Culture.Name;

            if (userId == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            try
            {

                code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
                var result = await _userManager.ConfirmEmailAsync(user, code);

                Status = 200;
                StatusMessage = culture == "ar" ? "شكراً لتأكيد بريدك الإلكتروني." : "Thank you for confirming your email.";
                if (!result.Succeeded)
                {
                    Status = 400;
                    StatusMessage = culture == "ar" ? "حدث خطأ أثناء تأكيد بريدك الإلكتروني." : "Error confirming your email.";
                }
            
            }
            catch
            {
                Status = 400;
                StatusMessage = culture == "ar" ? "حدث خطأ أثناء تأكيد بريدك الإلكتروني." : "Error confirming your email.";
            }
           
            return Page();
        }
    }
}
