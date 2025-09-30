using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TrandingSystem.Application.Features.Courses.Queries;
using TrandingSystem.Application.Features.Dashboard.Queries;
using TrandingSystem.Application.Features.Users.Queries;
using TrandingSystem.Domain.Entities;

namespace TrandingSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly IMediator _mediator;
        private readonly UserManager<User> _userManager;

        public DashboardController(IMediator mediator, UserManager<User> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }
        public async Task<IActionResult> KYC()
        
        
        
        {
            var usersResponse = await _mediator.Send(new GetAllUsersQuery());

            var AllCourses = await _mediator.Send(new GetAllCoursesQuery
            {
                UserId = 0
            });
            ViewBag.AllCourses = AllCourses.Data;

               
            return View(usersResponse.Data);
        }

        [HttpGet]
        public IActionResult FilterKycUser(int Userid=0,int CourseId=0,int Status = 0)
        {
            var KycUsers = _mediator.Send(new GetUserDataKycQuery(Userid, 0, Status)).Result;

            return PartialView("_KycUsersPartial", KycUsers);
        }
    }
}
