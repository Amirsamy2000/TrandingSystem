using Microsoft.AspNetCore.Mvc;
using TrandingSystem.Application.Features.Courses.Commands;
using AutoMapper;
using MediatR;
using TrandingSystem.Application.Features.Category.Queries;

namespace TrandingSystem.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CategoriesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Read()
        {
            var Rescourses = await _mediator.Send(new GetAllCategoriesQuery());
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
    }
}
