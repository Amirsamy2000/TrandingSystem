using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using TrandingSystem.Application.Features.Video.Queries;


namespace TrandingSystem.Controllers
{
    public class VideosController : Controller
    {
        private readonly IMediator _mediator;

        public VideosController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // this View For Display All Videos For Course Use CourseId
        public IActionResult Videos(int CourseId,CancellationToken cancellationToken)
        {
            var culture = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            //@ViewData["Master"] = @locaizer[""]
            var response= _mediator.Send(new GetVideosByCourseIdQuery(1, culture), cancellationToken);
            if (!response.Result.Success)
            {
               // return to Erro Page
            }
           
         
            return View(response.Result.Data);
        }
    }
}
