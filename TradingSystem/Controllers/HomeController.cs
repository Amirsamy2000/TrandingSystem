using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Security.Claims;
using TrandingSystem.Application.Features.Courses.Queries;
using TrandingSystem.Domain.Entities;
using TrandingSystem.Infrastructure.Data;
using TrandingSystem.Models;

using AutoMapper;
using MediatR;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly db23617Context _db;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger,     db23617Context options, IMediator mediator, IMapper mapper)
        {
            _logger = logger;
            _db = options;
            _mediator = mediator;
            _mapper = mapper;
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

        public async Task<IActionResult> Courses()
        {
            var result = await _mediator.Send(new GetAllCoursesQuery());
            return View(result.Data);
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

        [Authorize]
        public IActionResult Dashboard() { return View(); }

        public IActionResult Error(CustomeErrorModel error)
        {
          return View(error);
        }
    }
}
