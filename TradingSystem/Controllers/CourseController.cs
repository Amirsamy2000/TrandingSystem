using Microsoft.AspNetCore.Mvc;
using TrandingSystem.ViewModels;

namespace TrandingSystem.Controllers
{
    public class CourseController : Controller
    {
        public IActionResult Index()
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
            
            return View();

        }

    }
}
