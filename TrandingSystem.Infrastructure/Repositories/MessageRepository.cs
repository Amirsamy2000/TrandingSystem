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

        public async Task< bool > deleteMessage(int messageId)
        {
            _context.Messages.Remove(_context.Messages.Find(messageId));
            await _context.SaveChangesAsync();
            return true;
        }

        //TODO : remove count limit or make it configurable
        public async Task<List<Message>> GetRecentAsync(int communityId, int count = 30)
        {
            // Eager load User for each message
            return await _context.Messages
                .Include(m => m.User)
                .Where(m => m.CommunityId == communityId)
                .OrderByDescending(m => m.SentAt)
                //.Take(count)
                .OrderBy(m => m.SentAt)
                .ToListAsync();
        }





    }
}
