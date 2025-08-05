using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using TrandingSystem.Application.Resources;
using TrandingSystem.Domain.Interfaces;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text.Json;
namespace TrandingSystem.Controllers
{
    public class TestController : Controller
    {
        private readonly IStringLocalizer<ValidationMessages> _localizer;
        private readonly IFileService imageService;

      
        public TestController(IStringLocalizer<ValidationMessages> localizer ,IFileService imageService)
        {
            _localizer = localizer;
          
            this.imageService = imageService;
        }
        public IActionResult Index()
        {
            var msg = _localizer["Required_TitleAR"];

            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Upload(IFormFile Video)
        {
            var image = imageService.SaveImageAsync(Video);
            //if (Video == null || Video.Length == 0)
            //{
            //    return BadRequest("No video file selected.");
            //}

            //var libraryId = "475986"; // استبدلها بالقيمة الفعلية
            //var apiKey = "634e056f-4d4b-4e1b-8b48e0c99b52-f962-424c";       // استبدلها بالقيمة الفعلية
            //var videoTitle = Path.GetFileNameWithoutExtension(Video.FileName);

            //using var client = new HttpClient();
            //client.DefaultRequestHeaders.Add("AccessKey", apiKey);

            //// Step 1: Create video entry
            //var createRes = await client.PostAsJsonAsync($"https://video.bunnycdn.com/library/{libraryId}/videos", new
            //{
            //    title = videoTitle
            //});

            //if (!createRes.IsSuccessStatusCode)
            //{
            //    return BadRequest("Failed to create video entry in Bunny.net.");
            //}

            //var json = await createRes.Content.ReadAsStringAsync();
            //using var doc = JsonDocument.Parse(json);
            //string videoId = doc.RootElement.GetProperty("guid").GetString();

            //// Step 2: Upload actual video content
            //var uploadUrl = $"https://video.bunnycdn.com/library/{libraryId}/videos/{videoId}";

            //using var memoryStream = new MemoryStream();
            //await Video.CopyToAsync(memoryStream);
            //memoryStream.Position = 0;

            //var content = new StreamContent(memoryStream);
            //content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            //var uploadRes = await client.PutAsync(uploadUrl, content);

            //if (!uploadRes.IsSuccessStatusCode)
            //{
            //    return BadRequest("Failed to upload video content to Bunny.net.");
            //}

            //// ✅ You can now return the videoId for future use
            //ViewBag.VideoId = videoId;

            return View(); // أو RedirectToAction حسب استخدامك
        }







    }
}
