using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TrandingSystem.Application.Dtos;
using TrandingSystem.Application.Features.Courses.Queries;
using TrandingSystem.Application.Features.LiveSessions.Commands;
using TrandingSystem.Application.Features.LiveSessions.Queries;

namespace TrandingSystem.Controllers
{
    public class LiveSessionController : Controller
    {
        private readonly IMediator _mediator;
        public LiveSessionController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public IActionResult LiveSessions(CancellationToken cancellationToken, int CourseId = 1)
        {
            var culture = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            var response = _mediator.Send(new GetLiveSessionsByCourseIdQuery(CourseId, culture), cancellationToken).Result;
            
            ViewBag.CourseId = CourseId;
            ViewBag.CourseName = response.Message;
            return View(response.Data);
        }

        public IActionResult AddNewLiveSession(int CourseId = 0)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            int userId = int.Parse(userIdClaim.Value);

            var Rescourses = _mediator.Send(new GetAllCoursesQuery { UserId = userId }).Result;

            TempData["Course"] = Rescourses.Data;
            TempData["IsUpdatedPartial"] = 1;
            return View();
        }

        // this Partial View For Display Add New Video Form
        public IActionResult PartialViewAddNewLiveSession()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            int userId = int.Parse(userIdClaim.Value);

            var Rescourses = _mediator.Send(new GetAllCoursesQuery { UserId = userId }).Result;

            TempData["Course"] = Rescourses.Data;



            return PartialView("_PartialAddLiveSession");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> SubmitAddNewLiveSession(LiveSessionAddDto newSession, CancellationToken cancellationToken)
        {
            int UserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var res = await _mediator.Send(new AddNewLiveSessionCommand(UserId, newSession), cancellationToken);

            return Json(res);
        }

        // Dsiplay Partial Update Live Session
        public IActionResult PartialUpdateLiveSession(int SesstionId)
        {

            // get  Coursers
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            int userId = int.Parse(userIdClaim.Value);

            var Rescourses = _mediator.Send(new GetAllCoursesQuery { UserId = userId }).Result;

            TempData["Course"] = Rescourses.Data;
            TempData["IsUpdatedPartial"] = 1;
            // Get Data Live Session
            var resp = _mediator.Send(new GetLiveSessionBySessionIdQuery(SesstionId), CancellationToken.None).Result;


            return PartialView("_PartiaUpdateLiveSession", resp.Data);
        }
        [HttpPost]
        public IActionResult SubmitUpdateLiveSession(LiveSessionAddDto liveSessionUpdateDto)
        {
            var res = _mediator.Send(new UpdateLiveSessionCommand(liveSessionUpdateDto)).Result;
            return Json(res);
        }

        [HttpPost]
        public IActionResult DeleteLiveSession(int sessionId)
        {
            var res = _mediator.Send(new DeleteLiveSessionCommand(sessionId)).Result;
            return Json(res);
        }

        [HttpPost]
        public IActionResult BlockLiveSession(int SessionId,bool Status)
        {
            var res = _mediator.Send(new BlockLiveCommand(SessionId, Status)).Result;

            return Json(res);
        }

        [HttpPost]
        public IActionResult DeleteAllLiveSessions(int CourseId)
        {
            var res = _mediator.Send(new DeleteAllLivesCommand(CourseId)).Result;
            return Json(res);
        }
    }
}
