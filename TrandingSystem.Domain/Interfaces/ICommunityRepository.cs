using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrandingSystem.Domain.Abstractions;
using TrandingSystem.Domain.Entities;

namespace TrandingSystem.Domain.Interfaces
{
    public interface ICommunityRepository: IDomainInterface<Community>
    {
        IEnumerable<Community> GetAllCommunitiesByCourseId(int courseId);

        bool GetByTitle(string title);


        Task<bool> IsUserInCommunityAsync(int communityId, int userId);
        Task<Community> GetByIdAsync(int communityId);
        Task<List<Community>> GetForUserAsync(int userId);
        Task AddMemberAsync(int communityId, int userId);
        Task RemoveMemberAsync(int communityId, int userId);

       List<Community> GetAllCommunitiesByUserId(int UserId);
        // Add other CRUD as needed
    }
}
