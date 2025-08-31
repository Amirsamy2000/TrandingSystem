using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Dtos;
using TrandingSystem.Application.Features.Community.Commands;
using TrandingSystem.Application.Features.Community.Queries;
using TrandingSystem.Application.Features.Courses.Queries;
using TrandingSystem.Application.Features.Users.Queries;
using TrandingSystem.Domain.Entities;

namespace TrandingSystem.Controllers
{
    [Authorize]
    public class CommunitiesController : Controller
    {
        private readonly IMediator _mediator;
        public CommunitiesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Authorize(Roles = "Admin")]

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

        [Authorize(Roles = "Admin")]

        public IActionResult CreateCommunity()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]

        [HttpGet]
        public IActionResult PartialDisplayCommunities(int CourseId)
        {

            var resonse = _mediator.Send(new GellAllCommunityByCourseQuery(CourseId)).Result;
            return PartialView("_PartialDisplayCommunities", resonse.Data);
        }

        [Authorize(Roles = "Admin")]

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
        [Authorize(Roles = "Admin")]
        public IActionResult SubmitAddCommunity(CommunityCreateDto communityCreate)
        {
            var res = _mediator.Send(new AddNewCommunityCommand(communityCreate)).Result;
          
            return Json(res);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult SubmitCloseOrOpenCommunity(int CommunityId,bool IsClose)
        {
            var res = _mediator.Send(new CloseOrOpenCommunityCommand(CommunityId, IsClose)).Result;
            return Json(res);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult SubmitUpdateCommunity(int CommunityId, string Title,bool IsAdminOnly,CancellationToken cancellation)
        {
            var res = _mediator.Send(new UpdateCommunityCommand(Title, CommunityId, IsAdminOnly), cancellation).Result;
            return Json(res);

        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        public IActionResult PartialAssginUserIntoCommunity()
        {
           
             //Get Communities
            var AllCommunities = _mediator.Send(new GellAllCommunityByCourseQuery(0)).Result;
            return PartialView("_PartialAssginUserIntoCommunity",AllCommunities.Data);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult GetUsersOutCommunity(int communityId)
        {
            var response = _mediator.Send(new GetAllUsersByStatusQuery(4,communityId)).Result;
            
            return Json(response.Data);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult SubmitAssginUserIntoCommunity(List<int> usersId, int communityId)
        {
            var res = _mediator.Send(new AssginUserIntoCommunityCommand( communityId, usersId)).Result;
            return Json(res);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult PartialDisplayUsersCommunity(int communityId,string CommunityName)
        {
            ViewBag.CommunityName = CommunityName;
            ViewBag.Communityid = communityId;

            var response = _mediator.Send(new GetAllUsersByStatusQuery(5, communityId)).Result;

            return PartialView("_PartialDisplayUsersCoomunity", response.Data);

        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult SubmitDeleteUserFormCommunity(List<int> UserIds, int CommunityId)
        {
            var res = _mediator.Send( new DeleteUsersFormCommuntiyCommand(UserIds, CommunityId)).Result;
            return Json(res);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult SubmitBlockOrActiveUserFormCommunity(List<int> UserIds, int CommunityId,bool IsBlock)
        {
            var res = _mediator.Send(new BlockOrActiveUserCommand(UserIds, IsBlock, CommunityId)).Result;
            
            return Json(res);
        }
    }
}
