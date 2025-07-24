using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TrandingSystem.ViewModels;
using TrandingSystem.Application.Features.Courses.Commands;

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

        public async Task<IActionResult> Index()
        {
            var courses = await _mediator.Send(new GetAllCoursesQuery());
            return View(courses);
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
