using MediatR;
using Microsoft.AspNetCore.Mvc;
using TrandingSystem.Application.Features.LiveSessions.Queries;
using TrandingSystem.Application.Features.Video.Queries;

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
    }
}
