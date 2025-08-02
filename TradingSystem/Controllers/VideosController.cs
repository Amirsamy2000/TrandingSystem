using FluentValidation;
using Humanizer;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Security.Claims;
using System.Threading;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Dtos;
using TrandingSystem.Application.Features.Courses.Commands;
using TrandingSystem.Application.Features.Courses.Queries;
using TrandingSystem.Application.Features.Video.Commands;
using TrandingSystem.Application.Features.Video.Queries;
using TrandingSystem.Application.Validators;
using TrandingSystem.Domain.Entities;


namespace TrandingSystem.Controllers
{
    public class VideosController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IValidator<ViedoUpdateDto> _validator;

      
        public VideosController(IMediator mediator, IValidator<ViedoUpdateDto> validator)
        {
            _mediator = mediator;
            _validator = validator;
        }
        // this View For Display All Videos For Course Use CourseId
        public IActionResult Videos(CancellationToken cancellationToken, int CourseId = 1)
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
        public IActionResult PartialViewAddNewVideo()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            int userId = int.Parse(userIdClaim.Value);

            var Rescourses =  _mediator.Send(new GetAllCoursesQuery { UserId=userId}).Result;



            ViewBag.Courses = Rescourses;

            return PartialView("_PartialAddVideo", Rescourses.Data);
        }

        public IActionResult PartialViewUpdateNewVideo(int VideId ,int CourseId=0)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            int userId = int.Parse(userIdClaim.Value);
            // Get All Courses Data
            var Rescourses = _mediator.Send(new GetAllCoursesQuery{ UserId=userId}).Result;
  
            ViewBag.Courses = Rescourses.Data;
            ViewData["Courses"] = Rescourses.Data;
            ViewData["CourseId"] = CourseId;
            // Get  Video By VideoId for Update
            var response = _mediator.Send(new GetVideoByVideoIdQuery(VideId, Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName)).Result;
            return PartialView("_PartialUpdateVideo", response.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  async Task<IActionResult> SubmitUpdateVideo(ViedoUpdateDto UpdateedVideo)
        {
            var validationResult = await _validator.ValidateAsync(UpdateedVideo);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

                return PartialView("_PartialUpdateVideo", UpdateedVideo);
            }
            var response = _mediator.Send(new UpdateVideoCommand(UpdateedVideo, Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName)).Result;

            return Json(response);
        }

        // this View For Dispaly Partial View Add NEW Video

        public IActionResult AddNewVideo(int CourseId = 0)
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult BlockVideo(int VideoId,bool Status)
        {

            var response = _mediator.Send(new BlockVideoByIdCommand(VideoId, Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName, Status)).Result;

            return Json(response);
        }

        [HttpPost]
        public IActionResult DeleteAllVideos(int CourseId)
        {

            var response = _mediator.Send(new DeleteAllVideosByCourseIdCommand(CourseId, Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName)).Result;

            return Json(response);
        }

        [HttpPost]
        public IActionResult DeleteVideo(int VideoId,CancellationToken cancellationToken)
        {
            var reponse = _mediator.Send(new DeleteVideoByIdCommand(VideoId, Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName), cancellationToken).Result;

            return Json(reponse);
        }
        public IActionResult DeleteAllVideos(int VideoId, int Status)
        {

            return Json(Response<string>.SuccessResponse(
                        message: "Failed to block the video.",
                        data: "Video blocked successfully."
                    ));
        }

        [HttpGet]
        public IActionResult DisplayVideo(int VideoId)
        {
           
            return Json(Response<string>.SuccessResponse(
                         message: "Failed to block the video.",
                         data: "https://www.youtube.com/embed/lUVepezRfRw?list=PLc50qE8XmwQSRmaVLJxje3tEnU2ROGK_x"
                     ));

        }


    }
}
