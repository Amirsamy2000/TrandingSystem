using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrandingSystem.Domain.Entities;
using TrandingSystem.Domain.Interfaces;
using TrandingSystem.Infrastructure.Data;

namespace TrandingSystem.Infrastructure.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly db23617Context _context;
        public MessageRepository(db23617Context context)
        {
            _context = context;
        }

        public async Task<Message> AddAsync(Message message)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
            return message;
        }

        public async Task<List<Message>> GetRecentAsync(int communityId, int count = 30)
        {
            return await _context.Messages
                .Include(m => m.User)
                .Where(m => m.CommunityId == communityId)
                .OrderByDescending(m => m.SentAt)
                .OrderBy(m => m.SentAt)
                .ToListAsync();
        }

        public async Task<Message> GetByIdAsync(int id)
        {
            return await _context.Messages.Include(m => m.User).FirstOrDefaultAsync(m => m.MessageId == id);
        }

        public async Task DeleteAsync(int id)
        {
            var message = await _context.Messages.FindAsync(id);
            if (message != null)
            {
                _context.Messages.Remove(message);
                await _context.SaveChangesAsync();
            }
        }
    }
}
