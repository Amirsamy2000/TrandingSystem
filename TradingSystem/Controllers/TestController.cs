using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using TrandingSystem.Application.Resources;

namespace TrandingSystem.Controllers
{
    public class TestController : Controller
    {
        private readonly IStringLocalizer<ValidationMessages> _localizer;

        public TestController(IStringLocalizer<ValidationMessages> localizer)
        {
            _localizer = localizer;
        }
        public IActionResult Index()
        {
            var msg = _localizer["Required_TitleAR"];

            return View();
        }
    }
}
