using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Features.Courses.Commands;
using TrandingSystem.Domain.Entities;
using TrandingSystem.ViewModels;

namespace TrandingSystem.Controllers
{
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
            var Rescourses = await _mediator.Send(new GetAllCoursesQuery());
            // Check if the response is successful
            //if (!courses.Success)
            //{
            //    // Handle the error case, e.g., log the error or return an error view
            //    ModelState.AddModelError(string.Empty, courses.Message);
            //    return Json(new List<CourseVM>()); // Return an empty list or handle as needed
            //}
            //var courseViewModels = _mapper.Map<List<CourseVM>>(Rescourses);
            // Return the view with the list of courses

            //// Map the course entities to view models if necessary

            return Ok(Rescourses);
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
        public async Task<IActionResult> Create([FromBody] CourseVM model)
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
    }
}
