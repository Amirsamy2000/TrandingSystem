using Microsoft.AspNetCore.Mvc;

namespace TrandingSystem.Controllers
{
    public class VideosController : Controller
    {

        // this View For Display All Videos For Course Use CourseId
        public IActionResult Videos(int CourseId)
        {
            var culture = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            //@ViewData["Master"] = @locaizer[""]

            return View();
        }
    }
}
