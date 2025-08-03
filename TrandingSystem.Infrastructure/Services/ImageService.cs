using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TrandingSystem.Domain.Interfaces;
using TrandingSystem.Infrastructure.Constants;

namespace TrandingSystem.Infrastructure.Services
{
    public class ImageService : IImageService
    {
        private readonly string _imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UploadVideos");

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

        public Task<bool> DeleteVideoAsync(string videoFileName)
        {
            throw new NotImplementedException();
        }

        public async Task<string> SaveImageAsync(IFormFile imageFile, string existingFileName = null)
        {
            // id Path Is Not Found Create it
            if (!Directory.Exists(_imagePath))
                Directory.CreateDirectory(_imagePath);

            // Creat Image Name
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            var path = Path.Combine(_imagePath, fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            /// After save New Image Delete Old Image
            if (!string.IsNullOrEmpty(existingFileName))
                await DeleteImageAsync(existingFileName);

           return Path.Combine("UploadVideos", fileName).Replace("\\", "/");
        }

        public async Task<string> SaveVideoAsync(IFormFile videoFile)
        {
            {

                if (videoFile == null || videoFile.Length == 0)
                {
                    return "";
                }

                var libraryId = BunnaySetting.libraryId; // استبدلها بالقيمة الفعلية
                var apiKey = BunnaySetting.apiKey;       // استبدلها بالقيمة الفعلية
                var videoTitle = Path.GetFileNameWithoutExtension(videoFile.FileName);

                using var client = new HttpClient();
                client.DefaultRequestHeaders.Add("AccessKey", apiKey);

                // Step 1: Create video entry
                var createRes = await client.PostAsJsonAsync($"https://video.bunnycdn.com/library/{libraryId}/videos", new
                {
                    title = videoTitle
                });

                if (!createRes.IsSuccessStatusCode)
                {
                    return"";
                }

                var json = await createRes.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(json);
                string videoId = doc.RootElement.GetProperty("guid").GetString();

                // Step 2: Upload actual video content
                var uploadUrl = $"https://video.bunnycdn.com/library/{libraryId}/videos/{videoId}";

                using var memoryStream = new MemoryStream();
                await videoFile.CopyToAsync(memoryStream);
                memoryStream.Position = 0;

                var content = new StreamContent(memoryStream);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                var uploadRes = await client.PutAsync(uploadUrl, content);

                if (!uploadRes.IsSuccessStatusCode)
                {
                    return "";
                }

                return videoId; // Return the videoId for future use

            }
        }
    }
}
