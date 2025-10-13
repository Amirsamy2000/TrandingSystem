// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using TrandingSystem.Domain.Entities;
using TrandingSystem.Infrastructure.Data;

namespace TrandingSystem.Areas.Identity.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<LogoutModel> _logger;
        private readonly db23617Context _dbContext;

        public LogoutModel(SignInManager<User> signInManager, ILogger<LogoutModel> logger, db23617Context dbContext)
        {
            _signInManager = signInManager;
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            // Set IsOnline = false for all connections of the current user
            if (User.Identity.IsAuthenticated)
            {
                var userIdStr = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (int.TryParse(userIdStr, out int userId))
                {
                    var connections = _dbContext.UsersConnections.Where(x => x.UserId == userId && x.IsOnline == true).ToList();
                    foreach (var conn in connections)
                    {
                        conn.IsOnline = false;
                    }
                    await _dbContext.SaveChangesAsync();
                }
            }

            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                // This needs to be a redirect so that the browser performs a new
                // request and the identity for the user gets updated.
                return RedirectToAction("index","Home");
            }
        }
    }
}
