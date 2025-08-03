using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrandingSystem.Domain.Interfaces;

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
    }
}
