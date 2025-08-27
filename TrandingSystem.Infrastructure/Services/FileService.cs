using Microsoft.AspNetCore.Http;
using RestSharp;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using TrandingSystem.Domain.Entities;
using TrandingSystem.Domain.Interfaces;
using TrandingSystem.Infrastructure.Constants;

namespace TrandingSystem.Infrastructure.Services
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

    public class FileService : IFileService
    {
        private readonly string _imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UploadVideos");
        private readonly string folderId = "03803e3c9e4e4212a0eba466bb15566e";
        private readonly string apiKey = "F9lLNuRtnsl8Oju9LHuybHxPge4b76zhuYp5yK0DJpvfwaYoaYqmO0A8eF1dr6DE"; // ضع الـ API Key هنا


        public async Task<bool> DeleteImageAsync(string relativePath)
        {
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", relativePath.TrimStart('/'));
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteVideoAsync(string VideoId)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://dev.vdocipher.com/api/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Apisecret", apiKey);

                HttpResponseMessage response = await client.DeleteAsync($"videos?videos={VideoId}");

                return response.IsSuccessStatusCode;
            }
          
        }

        public string GenerateBunnyToken(string libraryId, string videoId, string signingKey)
        {
            var expires = DateTimeOffset.UtcNow.AddMinutes(60).ToUnixTimeSeconds();

            // التوقيع الصحيح: key + videoId + expires
            var payload = signingKey + videoId + expires;

            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var hash = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(payload));
            var token = BitConverter.ToString(hash).Replace("-", "").ToLower();

            // رابط المشغل الرسمي من Bunny
            var url = $"https://iframe.mediadelivery.net/embed/{libraryId}/{videoId}" +
                      $"?token={token}&expires={expires}" +
                      $"&autoplay=true&loop=false&muted=false&preload=false&responsive=true";

            return url;
        }
        public async Task<string> GetVideoUrlAsync(string videoId)
        {
            using (var _httpClient = new HttpClient())
            {
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
                return iframeUrl;

            }
                
        }

        public async Task<string> SaveImageAsync(IFormFile imageFile, string imagePath, string existingFileName = null)
        {
            // Ensure directory exists
            if (!Directory.Exists(imagePath))
                Directory.CreateDirectory(imagePath);

            // Create new image name
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine(imagePath, fileName);

            // Save new file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            // Delete old file if exists
            if (!string.IsNullOrEmpty(existingFileName))
                await DeleteImageAsync(existingFileName);

            // Return relative path for browser (e.g., /CoursesImages/fileName.png)
            var relativePath = "/" + Path.GetRelativePath(
                Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"),
                filePath
            ).Replace("\\", "/");

            return relativePath;
        }

        // Old Funtion Save in servr bunnay
        //public async Task<string> SaveVideoAsync(IFormFile videoFile)
        //{
        //    {

        //        if (videoFile == null || videoFile.Length == 0)
        //        {
        //            return "";
        //        }

        //        var libraryId = BunnaySetting.libraryId; // استبدلها بالقيمة الفعلية
        //        var apiKey = BunnaySetting.apiKey;       // استبدلها بالقيمة الفعلية
        //        var videoTitle = Path.GetFileNameWithoutExtension(videoFile.FileName);

        //        using var client = new HttpClient();
        //        client.Timeout = TimeSpan.FromMinutes(10);
        //        client.DefaultRequestHeaders.Add("AccessKey", apiKey);

        //        // Step 1: Create video entry
        //        var createRes = await client.PostAsJsonAsync($"https://video.bunnycdn.com/library/{libraryId}/videos", new
        //        {
        //            title = videoTitle
        //        });

        //        if (!createRes.IsSuccessStatusCode)
        //        {
        //            return"";
        //        }

        //        var json = await createRes.Content.ReadAsStringAsync();
        //        using var doc = JsonDocument.Parse(json);
        //        string videoId = doc.RootElement.GetProperty("guid").GetString();

        //        // Step 2: Upload actual video content
        //        var uploadUrl = $"https://video.bunnycdn.com/library/{libraryId}/videos/{videoId}";

        //        using var memoryStream = new MemoryStream();
        //        await videoFile.CopyToAsync(memoryStream);
        //        memoryStream.Position = 0;

        //        var content = new StreamContent(memoryStream);
        //        content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

        //        var uploadRes = await client.PutAsync(uploadUrl, content);

        //        if (!uploadRes.IsSuccessStatusCode)
        //        {
        //            return "";
        //        }

        //        return videoId; // Return the videoId for future use

        //    }
        //}

        // new Funtion Save in server vdochiper
        public async Task<string> SaveVideoAsync(IFormFile video)
        {

            if (video == null || video.Length == 0)
                return "";

            // ==== Step 1: Get Upload Credentials using RestSharp ====
            var title = Guid.NewGuid();

            var client = new RestClient($"https://dev.vdocipher.com/api/videos?title={title}&folderId={folderId}");
            var request = new RestRequest("", Method.Put);
            request.AddHeader("Authorization", $"Apisecret {apiKey}");

            RestResponse response = client.Execute(request);

            if (!response.IsSuccessful)
                return "";

            // Parse JSON
            var credentials = JsonSerializer.Deserialize<UploadCredentialsResponse>(response.Content);
            if (credentials == null || credentials.ClientPayload == null)
                return "";

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
                return videoId;

            var errorContent = await uploadResponse.Content.ReadAsStringAsync();
            return "";


        }

    }
}
