using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Features.Category.Queries;
using TrandingSystem.Application.Features.Courses.Commands;

namespace TrandingSystem.Controllers
{
    [Authorize(Roles = "Admin")]

    public class CategoriesController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CategoriesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var Rescourses = await _mediator.Send(new GetAllCategoriesQuery());
            return View(Rescourses.Data);
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

        [HttpPost]
        public async Task<IActionResult> SubmitAddNewCat(string cateenName,string catearName)
        {
            var res = Response<string>.SuccessResponse("good","good");
            return Json(res);
        }


        [HttpPost]
        public async Task<IActionResult> SubmitUpdateCat(int catid, string cateenName, string catearName)
        {
            var res = Response<string>.SuccessResponse("good", "good");
            return Json(res);
        }
    }
}
