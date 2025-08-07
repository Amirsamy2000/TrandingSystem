
using Microsoft.AspNetCore.Http;

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
