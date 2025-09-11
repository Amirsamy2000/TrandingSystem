using FluentValidation;
using Humanizer;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Dtos;
using TrandingSystem.Application.Features.Courses.Commands;
using TrandingSystem.Application.Features.Courses.Queries;
using TrandingSystem.Application.Features.Video.Commands;
using TrandingSystem.Application.Features.Video.Handlers;
using TrandingSystem.Application.Features.Video.Queries;
//using TrandingSystem.Application.Validators;
using TrandingSystem.Domain.Entities;


namespace TrandingSystem.Controllers
{
    [Authorize]
    public class VideosController : Controller
    {
        private readonly IMediator _mediator;
      //  private readonly IValidator<ViedoUpdateDto> _validator;
        //private readonly IValidator<VideoAddedDto> _validator2;


        public VideosController(IMediator mediator)
        {
            _mediator = mediator;
           // _validator = validator;
            
        }
        // this View For Display All Videos For Course Use CourseId
        [Authorize(Roles = "Lecturer,Admin")]
        public IActionResult Videos(CancellationToken cancellationToken, int CourseId = 1)
        {
            var culture = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            //@ViewData["Master"] = @locaizer[""]
            var response= _mediator.Send(new GetVideosByCourseIdQuery(CourseId, culture), cancellationToken).Result;
         

            ViewBag.CourseId = CourseId;
            ViewBag.CourseName = response.Message;
            return View(response.Data);
        }

        [HttpGet]
        public IActionResult PartialVideos(CancellationToken cancellationToken, int CourseId = 1)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found in token.");
            }
            int userId = int.Parse(userIdClaim.Value);
            var culture = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;


            //@ViewData["Master"] = @locaizer[""]
            var response = _mediator.Send(new GetLivesAndVideosByUserEnrollmentQuery(userId, culture, CourseId), cancellationToken).Result;
            if (!response.Success)
            {
                // return to Erro Page
                return PartialView("_ErrorPartialView");
            }
            //  ViewData["CourseName"] = response.Result.Data2.ToString();

            ViewBag.CourseId = CourseId;
            ViewBag.CourseName = response.Message;

            return PartialView("_VideosPartialView", response.Data);
        }



        // this Partial View For Display Add New Video Form
        [Authorize(Roles = "Lecturer,Admin")]
        public IActionResult PartialViewAddNewVideo()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            int userId = int.Parse(userIdClaim.Value);

            var Rescourses = _mediator.Send(new GetAllCoursesQuery { UserId = userId }).Result;

            TempData["Course"] = Rescourses.Data;
            return PartialView("_PartialAddVideo");
        }

        [HttpGet]
        [Authorize(Roles = "Lecturer,Admin")]
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
        [Authorize(Roles = "Lecturer,Admin")]
        public async Task<IActionResult> SubmitUpdateVideo([FromForm] ViedoUpdateDto UpdateedVideo)
        {
            //var validationResult = await _validator.ValidateAsync(UpdateedVideo);
            //if (!validationResult.IsValid)
            //{
            //    foreach (var error in validationResult.Errors)
            //        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

            //    return PartialView("_PartialUpdateVideo", UpdateedVideo);
            //}
            var response = _mediator.Send(new UpdateVideoCommand(UpdateedVideo, Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName)).Result;

            return Json(response);
        }

        // this View For Dispaly Partial View Add NEW Video

        [Authorize(Roles = "Lecturer,Admin")]
        public IActionResult AddNewVideo(int CourseId = 0)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            int userId = int.Parse(userIdClaim.Value);

            var Rescourses = _mediator.Send(new GetAllCoursesQuery { UserId = userId }).Result;

            TempData["Course"] = Rescourses.Data;
            return View();
        }

        // Submit Add New Video
        [ValidateAntiForgeryToken]
        [HttpPost]
        [Authorize(Roles = "Lecturer,Admin")]
        public async Task<IActionResult> SubmitAddNewVideo(  VideoAddedDto newVideo)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            int userId = int.Parse(userIdClaim.Value);
          
           var response = _mediator.Send(new AddNewVideoCommand(newVideo, userId)).Result;

            return Json(response);
        }

        [HttpPost]
        [Authorize(Roles = "Lecturer,Admin")]
        public IActionResult BlockVideo(int VideoId,bool Status)
        {

            var response = _mediator.Send(new BlockVideoByIdCommand(VideoId, Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName, Status)).Result;

            return Json(response);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteAllVideos(int CourseId)
        {

            var response = _mediator.Send(new DeleteAllVideosByCourseIdCommand(CourseId, Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName)).Result;

            return Json(response);
        }

        [HttpPost]
        [Authorize(Roles = "Lecturer,Admin")]
        public IActionResult DeleteVideo(int VideoId,CancellationToken cancellationToken)
        {
            var reponse = _mediator.Send(new DeleteVideoByIdCommand(VideoId, Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName), cancellationToken).Result;

            return Json(reponse);
        }
        [Authorize(Roles = "Admin")]
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
            var response = _mediator.Send(new GetSingnedUrlVideoQuery(VideoId)).Result;
          
            return Json(response);

        }

        [HttpPost]
        public async Task<IActionResult> EnrollVideo(int id, IFormFile RecieptImage)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            int userId = int.Parse(userIdClaim.Value);
            var response =await _mediator.Send(new EnrollmentInPaidVideoCommand(id, RecieptImage, userId));
            return Json(response);

        }

    }
}
