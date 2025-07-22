using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using TrandingSystem.Infrastructure.Data;
using TrandingSystem.Domain.Entities;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly db23617Context _db;

        public HomeController(ILogger<HomeController> logger,     db23617Context options)
        {
            _logger = logger;
            _db = options;
        }

        public IActionResult Index()
        {

         //   var t = _db.Roles.ToList();

            return View();

           
        }
        public IActionResult About()
        {
            return View();
        }

        public IActionResult Pricing()
        {
            return View();
        }

        public IActionResult Events()
        {
            return View();
        }

        public IActionResult Trainers()
        {
            return View();
        }

        public IActionResult Courses()
        {
            return View();
        }

        public IActionResult CoursesDetails()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            // إعداد الكوكي الخاصة باللغة الجديدة
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            // إعادة توجيه المستخدم لنفس الصفحة
            return LocalRedirect(returnUrl);
        }

        public IActionResult Dashboard() { return View(); }
    }
}
