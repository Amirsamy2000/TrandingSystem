using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.Extensions.Localization;
using TrandingSystem.Application.Features.analysis.Queriers;
using TrandingSystem.Application.Features.Courses.Queries;
using TrandingSystem.Controllers;
using TrandingSystem.Domain.Entities;
using TrandingSystem.Infrastructure.Data;
using TrandingSystem.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly db23617Context _db;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IStringLocalizer<CommunitiesController> _localizer;
        public HomeController(ILogger<HomeController> logger,     db23617Context options, IMediator mediator, IMapper mapper, UserManager<User> userManager
            , IStringLocalizer<CommunitiesController> localizer)
        {
            _logger = logger;
            _db = options;
            _mediator = mediator;
            _mapper = mapper;
            _userManager = userManager;
            _localizer = localizer;
        }

        public IActionResult Index()
        {

            //   var t = _db.Roles.ToList();
            // Get Anal of Count Course and stds ,video ,lec

            var result =  _mediator.Send(new GetCountStds_Courses_Videos_lecQuery()).Result;

            return View(result);

           
        }

        public IActionResult About()
        {
            var result = _mediator.Send(new GetCountStds_Courses_Videos_lecQuery()).Result;

            return View(result);
        }

        [Authorize(Roles ="Admin")]
        public IActionResult Pricing()
        {
            return View();
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Events()
        {
            return View();
        }

        public IActionResult Test()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Trainers()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Courses()
        {
            var result = await _mediator.Send(new GetAllCoursesQuery());
            return View(result.Data);
        }

        [Authorize]
        public IActionResult GoToCourse(   int CourseId = 0)
        {
            if (CourseId == 0)
            {
                return RedirectToAction("Courses", "Home");
            }
            var course  = _mediator.Send(new GetCourseByIdQuery{CourseId = CourseId}).Result;
            return View(course.Data);
        }

        [Authorize]
        public IActionResult CourseDetails(int CourseId, CancellationToken cancellationToken)
        {
            var course  = _mediator.Send(new GetCourseByIdQuery{CourseId = CourseId}, cancellationToken).Result;
            return View(course.Data);
        }

        [Authorize]
        public IActionResult Contact()
        {
         
            var user =  _userManager.GetUserAsync(User).Result;
            var email =  _userManager.GetEmailAsync(user).Result;
            ViewBag.Email = email??"";
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

        [Authorize(Roles = "Admin")]
        public IActionResult Dashboard() { return View(); }

        public IActionResult Error(int? statusCode = null)
        {
            var model = new CustomeErrorModel();

            if (statusCode.HasValue)
            {
                model.Status = statusCode.Value;

                model.Message = statusCode.Value switch
                {
                    404 => _localizer["Error404"],
                    500 => _localizer["Error500"],
                    403 => _localizer["Error403"],
                    _ => _localizer["ErrorUn"]
                };
            }
            else
            {
                model.Status = 500;
                model.Message = _localizer["ErrorUn"];
            }

            return View(model);
        }

        public IActionResult LockedAccount()
        {            
            return View();
        }

        public IActionResult PartialUploadReceiptVideo(int id)
        {
            return PartialView("_PartialUploadReceiptVideo",id);
        }

        public IActionResult PartialUploadReceiptSession(int id)
        {
            return PartialView("_PartialUploadReceiptSession", id);
        }
    }
}
