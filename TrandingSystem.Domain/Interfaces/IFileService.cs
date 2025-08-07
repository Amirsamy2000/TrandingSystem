using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TrandingSystem.Domain.Entities;

namespace TrandingSystem.Domain.Interfaces
{
    public interface IFileService
    {
        Task<string> SaveImageAsync(IFormFile imageFile, string ImagePath,string existingFileName = null);
        Task<bool> DeleteImageAsync(string fileName);

        Task<string> SaveVideoAsync(IFormFile videoFile);
        Task<bool> DeleteVideoAsync(string videoFileName);
        string GenerateBunnyToken(string libraryId, string videoId, string signingKey);



    }
}
