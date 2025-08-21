using System.Collections.Generic;
using System.Threading.Tasks;
using TrandingSystem.Domain.Entities;

namespace TrandingSystem.Domain.Interfaces
{
    public interface ICommunityRepository
    {
        Task<bool> IsUserInCommunityAsync(int communityId, int userId);
        Task<Community> GetByIdAsync(int communityId);
        Task<List<Community>> GetForUserAsync(int userId);
        Task AddMemberAsync(int communityId, int userId);
        Task RemoveMemberAsync(int communityId, int userId);
        // Add other CRUD as needed
    }
}
