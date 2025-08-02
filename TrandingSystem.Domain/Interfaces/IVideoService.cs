using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrandingSystem.Domain.Interfaces
{
    public interface IVideoService
    {
        Task<string> SaveVideoAsync(IFormFile videoFile, string existingFileName = null);
        Task<bool> DeleteVideoAsync(string fileName);
    }
}
