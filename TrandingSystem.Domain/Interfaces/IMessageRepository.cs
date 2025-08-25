using System.Collections.Generic;
using System.Threading.Tasks;
using TrandingSystem.Domain.Entities;

namespace TrandingSystem.Domain.Interfaces
{
    public interface IMessageRepository
    {
        Task<Message> AddAsync(Message message);
        Task<List<Message>> GetRecentAsync(int communityId, int count = 30);
    }
}
