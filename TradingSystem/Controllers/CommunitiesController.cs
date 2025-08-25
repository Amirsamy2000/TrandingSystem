using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TrandingSystem.Application.Dtos;
using TrandingSystem.Application.Features.Community.Commands;
using TrandingSystem.Application.Features.Community.Queries;
using TrandingSystem.Application.Features.Courses.Queries;

namespace TrandingSystem.Controllers
{
    public class CommunitiesController : Controller
    {
        private readonly IMediator _mediator;
        public CommunitiesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<IActionResult> Index()
        {
            // Display Courses
            // Get the current user ID from the claims
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            int userId = int.Parse(userIdClaim.Value);

            var courses = await _mediator.Send(new GetAllCoursesQuery
            {
                UserId = userId
            });



            return View(courses.Data);
        }


        public IActionResult CreateCommunity()
        {
            return View();
        }
        [HttpGet]
        public IActionResult PartialDisplayCommunities(int CourseId)
        {

            var resonse = _mediator.Send(new GellAllCommunityByCourseQuery(CourseId)).Result;
            return PartialView("_PartialDisplayCommunities", resonse.Data);
        }

        public async Task<IActionResult> PartialCreateNewCommunity()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized(); // بدل RedirectToAction
            }
            int userId = int.Parse(userIdClaim.Value);

            var courses = await _mediator.Send(new GetAllCoursesQuery
            {
                UserId = userId
            });

            ViewBag.Courses = courses.Data;

            return PartialView("_PartialCreateNewCommunity", new CommunityCreateDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult SubmitAddCommunity(CommunityCreateDto communityCreate)
        {
            var res = _mediator.Send(new AddNewCommunityCommand(communityCreate)).Result;
          
            return Json(res);
        }

        [HttpPost]
        public IActionResult SubmitCloseOrOpenCommunity(int CommunityId,bool IsClose)
        {
            var res = _mediator.Send(new CloseOrOpenCommunityCommand(CommunityId, IsClose)).Result;
            return Json(res);
        }

        [HttpPost]
        public IActionResult SubmitUpdateCommunity(int CommunityId, string Title,CancellationToken cancellation)
        {
            var res = _mediator.Send(new UpdateCommunityCommand(Title, CommunityId), cancellation).Result;
            return Json(res);

        }

        [HttpPost]
        public IActionResult SubmitDeleteCommunity(int CommunityId)
        {
            var res = _mediator.Send(new DeleteCommunityCommand(CommunityId)).Result;
            return Json(res);
        }

        public IActionResult ShowCommunitiesForUser()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized(); // بدل RedirectToAction
            }
            int userId = int.Parse(userIdClaim.Value);
            var response = _mediator.Send(new GetAllCommunityByUserQuery(userId)).Result;
            return View(response.Data);
        }
    }
}
