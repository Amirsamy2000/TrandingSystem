using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Features.Courses.Commands;
using TrandingSystem.Application.Features.Courses.Queries;
using TrandingSystem.Application.Features.Users.Queries;
using TrandingSystem.Domain.Entities;
using TrandingSystem.ViewModels;

namespace TrandingSystem.Controllers
{
    [Authorize]

    public class CourseController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CourseController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> ReadAll()
        {
            // Get the current user ID from the claims
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            int userId = int.Parse(userIdClaim.Value);

            var result = await _mediator.Send(new GetAllCoursesQuery
            {
                UserId = userId
            });

            //if (!result.Success)
            //    return StatusCode((int)(result.Status ?? System.Net.HttpStatusCode.BadRequest), result);

            return Ok(result);

            //var Rescourses = await _mediator.Send(new GetAllCoursesQuery());
            //// Check if the response is successful
            ////if (!courses.Success)
            ////{
            ////    // Handle the error case, e.g., log the error or return an error view
            ////    ModelState.AddModelError(string.Empty, courses.Message);
            ////    return Json(new List<CourseVM>()); // Return an empty list or handle as needed
            ////}
            ////var courseViewModels = _mapper.Map<List<CourseVM>>(Rescourses);
            //// Return the view with the list of courses

            ////// Map the course entities to view models if necessary

            //return Ok(Rescourses);
        }


        [HttpGet]
        public async Task<IActionResult> _ReadByIdPartialView(int CourseId)
        {
            // Get the current user ID from the claims
            
            var result = await _mediator.Send(new GetCourseByIdQuery
            {
                CourseId = CourseId
            });

            ViewBag.Course = result.Data;

            return PartialView("_ReadByIdPartialView");
        }

        [HttpGet]
        public async Task<IActionResult> AssignTeacherForCourse(int CourseId)
        {
            try
            {
                var result = await _mediator.Send(new ReadNotAssignedTeachersQuery
                {
                    CourseId = CourseId
                });

                if (!result.Success || result.Data == null)
                {
                    return PartialView("_ErrorPartial", "Failed to load teachers.");
                }

                // You can also pass CourseId to the view if needed
                ViewBag.CourseId = CourseId;
                ViewBag.AssignedLectures = result.Data2;

                return PartialView("_AssignTeacherForCourse", result.Data);
            }
            catch (Exception ex)
            {
                // Log the exception
                // _logger.LogError(ex, "Error loading teachers for course assignment");

                return PartialView("_ErrorPartial", "An error occurred while loading teachers.");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveTeacherFromCourse(int TeacherId,int CourseId)
        {
            try
            {
                Response<bool> result = await _mediator.Send(new RemoveTeacherFromCourseCommand
                {
                    CourseId = CourseId,
                    TeacherId = TeacherId
                });

                if (!result.Success || result.Data == null)
                {
                    return PartialView("_ErrorPartial", "Failed to remove Teacher.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception
                // _logger.LogError(ex, "Error loading teachers for course assignment");

                return PartialView("_ErrorPartial", "An error occurred while loading teachers.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AssignTeacherToCourse(int CourseId, List<int> TeachersId)
        {
            try
            {
                // Validate input
                if (CourseId <= 0)
                {
                    ModelState.AddModelError(string.Empty, "Invalid course selected.");
                    return BadRequest(ModelState);
                }

                if (TeachersId == null || !TeachersId.Any())
                {
                    ModelState.AddModelError(string.Empty, "At least one teacher must be selected.");
                    return BadRequest(ModelState);
                }

                // Remove duplicates and invalid IDs
                TeachersId = TeachersId.Where(id => id > 0).Distinct().ToList();

                if (!TeachersId.Any())
                {
                    ModelState.AddModelError(string.Empty, "No valid teachers selected.");
                    return BadRequest(ModelState);
                }

                var result = await _mediator.Send(new AssignTeacherToCourseCommand
                {
                    CourseId = CourseId,
                    TeachersId = TeachersId
                });

                if (!result.Success)
                {
                    ModelState.AddModelError(string.Empty, result.Message);

                    // If this is an AJAX request, return error as JSON
                    if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                    {
                        return Json(new { success = false, message = result.Message });
                    }

                    return NotFound();
                }

                // Success response
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new
                    {
                        success = true,
                        message = $"Successfully assigned {TeachersId.Count} teacher(s) to the course.",
                        redirectUrl = Url.Action("Index")
                    });
                }

                TempData["SuccessMessage"] = $"Successfully assigned {TeachersId.Count} teacher(s) to the course.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Log the exception
                // _logger.LogError(ex, "Error assigning teachers to course {CourseId}", CourseId);

                var errorMessage = "An error occurred while assigning teachers to the course.";

                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = false, message = errorMessage });
                }

                ModelState.AddModelError(string.Empty, errorMessage);
                return BadRequest(ModelState);
            }
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
           
        [HttpPost]
        public async Task<IActionResult> Create(CourseVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); // Re-display the form with validation messages
            }

            var command = _mapper.Map<AddCourseCommand>(model);

            var result = await _mediator.Send(command);

            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View(model);
            }

            // Redirect to index or success page
            return RedirectToAction("Create");
        }


        [HttpPost]
        public async Task<IActionResult> update(CourseVM model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index"); // Re-display the form with validation messages
            }

            var command = _mapper.Map<UpdateCourseCommand>(model);

            var result = await _mediator.Send(command);

            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return NotFound();
            }

            // Redirect to index or success page
            return RedirectToAction("Index");
        }

        


        [HttpPost]
        public async Task<IActionResult> EnrollCourse(int courseId, IFormFile? RecieptImage)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            int userId = int.Parse(userIdClaim.Value);

            var result = await _mediator.Send(new EnrollCourseCommand(courseId, userId, RecieptImage));

            return Json(result);
        }




        [HttpGet]
        public IActionResult UploadReceiptPartial(int courseId,bool IsFree)
        {
            ViewBag.IsFree = IsFree;
            return PartialView("_UploadReceiptPartial",courseId);

        }


        [HttpDelete]
        public async Task<IActionResult> Delete(int CourseId)
        {


            var result = await _mediator.Send(new DeleteCourseCommand
            {
                courseId = CourseId
            });

            return Ok(result);
        }
    }
}
