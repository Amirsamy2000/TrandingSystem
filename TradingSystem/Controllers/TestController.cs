using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using TrandingSystem.Application.Resources;
using TrandingSystem.Domain.Interfaces;
using System.Text.Json.Serialization;

using RestSharp;
using System.Text.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using TrandingSystem.Domain.Entities;
namespace TrandingSystem.Controllers
{

    public class UploadCredentialsResponse
    {
        [JsonPropertyName("clientPayload")]
        public ClientPayload ClientPayload { get; set; }

        [JsonPropertyName("videoId")]
        public string VideoId { get; set; }
    }

    public class ClientPayload
    {
        [JsonPropertyName("policy")]
        public string Policy { get; set; }

        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("x-amz-signature")]
        public string XAmzSignature { get; set; }

        [JsonPropertyName("x-amz-algorithm")]
        public string XAmzAlgorithm { get; set; }

        [JsonPropertyName("x-amz-date")]
        public string XAmzDate { get; set; }

        [JsonPropertyName("x-amz-credential")]
        public string XAmzCredential { get; set; }

        [JsonPropertyName("uploadLink")]
        public string UploadLink { get; set; }
    }


    public class TestController : Controller
    {
        private readonly IStringLocalizer<ValidationMessages> _localizer;
        private readonly IFileService imageService;
        private readonly HttpClient _httpClient=new HttpClient();

        public TestController(IStringLocalizer<ValidationMessages> localizer ,IFileService imageService)
        {
            _localizer = localizer;
          
            this.imageService = imageService;

           
        }

        [HttpGet]
        public async Task<IActionResult> GetOtp(string videoId)
        {
            string videoIdc = "9a327e1d3b924bcc9d0f86378566ce37";
            var apiKey = "F9lLNuRtnsl8Oju9LHuybHxPge4b76zhuYp5yK0DJpvfwaYoaYqmO0A8eF1dr6DE";
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Apisecret", apiKey); // 🔑 حط الـ API Secret هنا

            var response = await httpClient.PostAsync(
                $"https://dev.vdocipher.com/api/videos/{videoIdc}/otp", null);

            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return Content(json, "application/json");
        }
        public async Task<IActionResult> Index()
        {
            string videoId = "9a327e1d3b924bcc9d0f86378566ce37";
            var apiKey = "F9lLNuRtnsl8Oju9LHuybHxPge4b76zhuYp5yK0DJpvfwaYoaYqmO0A8eF1dr6DE"; // ضع الـ API Key هنا
            _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Apisecret", apiKey);

            var response = await _httpClient.PostAsync(
                $"https://dev.vdocipher.com/api/videos/{videoId}/otp", null);

            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();

            // نفك JSON عشان ناخد otp و playbackInfo
            using var doc = JsonDocument.Parse(json);
            var otp = doc.RootElement.GetProperty("otp").GetString();
            var playbackInfo = doc.RootElement.GetProperty("playbackInfo").GetString();

            // نرجع URL كامل جاهز للـ iframe
            var iframeUrl = $"https://player.vdocipher.com/v2/?otp={otp}&playbackInfo={playbackInfo}";
             ViewBag.IframeUrl = iframeUrl;

            return View();
        }
        //[HttpPost]

        //public async Task<IActionResult> Upload(IFormFile Video)
        //{
        //    var image = imageService.SaveImageAsync(Video, ConstantPath.PathVideoImage);
        //    //if (Video == null || Video.Length == 0)
        //    //{
        //    //    return BadRequest("No video file selected.");
        //    //}

        //    //var libraryId = "475986"; // استبدلها بالقيمة الفعلية
        //    //var apiKey = "634e056f-4d4b-4e1b-8b48e0c99b52-f962-424c";       // استبدلها بالقيمة الفعلية
        //    //var videoTitle = Path.GetFileNameWithoutExtension(Video.FileName);

        //    //using var client = new HttpClient();
        //    //client.DefaultRequestHeaders.Add("AccessKey", apiKey);

        //    //// Step 1: Create video entry
        //    //var createRes = await client.PostAsJsonAsync($"https://video.bunnycdn.com/library/{libraryId}/videos", new
        //    //{
        //    //    title = videoTitle
        //    //});

        //    //if (!createRes.IsSuccessStatusCode)
        //    //{
        //    //    return BadRequest("Failed to create video entry in Bunny.net.");
        //    //}

        //    //var json = await createRes.Content.ReadAsStringAsync();
        //    //using var doc = JsonDocument.Parse(json);
        //    //string videoId = doc.RootElement.GetProperty("guid").GetString();

        //    //// Step 2: Upload actual video content
        //    //var uploadUrl = $"https://video.bunnycdn.com/library/{libraryId}/videos/{videoId}";

        //    //using var memoryStream = new MemoryStream();
        //    //await Video.CopyToAsync(memoryStream);
        //    //memoryStream.Position = 0;

        //    //var content = new StreamContent(memoryStream);
        //    //content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

        //    //var uploadRes = await client.PutAsync(uploadUrl, content);

        //    //if (!uploadRes.IsSuccessStatusCode)
        //    //{
        //    //    return BadRequest("Failed to upload video content to Bunny.net.");
        //    //}

        //    //// ✅ You can now return the videoId for future use
        //    //ViewBag.VideoId = videoId;

        //    return View(); // أو RedirectToAction حسب استخدامك
        //}




        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile video)
        {
            if (video == null || video.Length == 0)
                return BadRequest("No video selected.");

            // ==== Step 1: Get Upload Credentials using RestSharp ====
            var apiKey = "F9lLNuRtnsl8Oju9LHuybHxPge4b76zhuYp5yK0DJpvfwaYoaYqmO0A8eF1dr6DE"; // ضع الـ API Key هنا
            var title = Guid.NewGuid();
            var folderId = "03803e3c9e4e4212a0eba466bb15566e";

            var client = new RestClient($"https://dev.vdocipher.com/api/videos?title={title}&folderId={folderId}");
            var request = new RestRequest("",Method.Put);
            request.AddHeader("Authorization", $"Apisecret {apiKey}");

            RestResponse response = client.Execute(request);

            if (!response.IsSuccessful)
                return StatusCode(500, "Error getting upload credentials: " + response.Content);

            // Parse JSON
            var credentials = JsonSerializer.Deserialize<UploadCredentialsResponse>(response.Content);
            if (credentials == null || credentials.ClientPayload == null)
                return StatusCode(500, "Failed to parse upload credentials.");

            var payload = credentials.ClientPayload;
            var videoId = credentials.VideoId;

            // ==== Step 2: Upload Video to S3 using HttpClient ====
            using var fs = new MemoryStream();
            await video.CopyToAsync(fs);
            fs.Position = 0;

            using var form = new MultipartFormDataContent();
            form.Add(new StringContent(payload.Key), "key");
            form.Add(new StringContent(payload.Policy), "policy");
            form.Add(new StringContent(payload.XAmzAlgorithm), "x-amz-algorithm");
            form.Add(new StringContent(payload.XAmzCredential), "x-amz-credential");
            form.Add(new StringContent(payload.XAmzDate), "x-amz-date");
            form.Add(new StringContent(payload.XAmzSignature), "x-amz-signature");
            form.Add(new StringContent("201"), "success_action_status");
            form.Add(new StringContent(""), "success_action_redirect");

            var fileContent = new StreamContent(fs);
            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(video.ContentType);
            form.Add(fileContent, "file", video.FileName);

            // ⚠️ بدون Authorization Header عند رفع الفيديو على S3
            using var httpClient = new HttpClient();
            var uploadResponse = await httpClient.PostAsync(payload.UploadLink, form);

            if (uploadResponse.StatusCode == System.Net.HttpStatusCode.Created)
                return Ok(new { videoId });

            var errorContent = await uploadResponse.Content.ReadAsStringAsync();
            return StatusCode((int)uploadResponse.StatusCode, "Upload failed: " + errorContent);
        }
    }
}








