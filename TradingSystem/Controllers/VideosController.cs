using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Threading;
using TrandingSystem.Application.Features.Video.Queries;
using TrandingSystem.Domain.Entities;


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
          //  ViewData["CourseName"] = response.Result.Data2.ToString();


            return View(response.Result.Data);
        }


        // this Partial View For Display Add New Video Form
        public IActionResult PartialViewAddNewVideo(int CourseId=0)
        {
             IEnumerable<Course> Course = null;
            if (CourseId != 0)
            {
                // Get All Courses Data
                //there
            }

            ViewData["CourseId"] = CourseId;

            return PartialView("_PartialAddVideo", Course);
        }

       // this View For Dispaly Partial View Add NEW Video

        public IActionResult AddNewVideo(int CourseId = 0)
        {
            
            return View();
        }


    }
}
