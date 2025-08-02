using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrandingSystem.Domain.Interfaces
{
    public interface IImageService
    {
        Task<string> SaveImageAsync(IFormFile imageFile, string existingFileName = null);
        Task<bool> DeleteImageAsync(string fileName);
    }
}
