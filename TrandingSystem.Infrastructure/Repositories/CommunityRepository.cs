using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrandingSystem.Domain.Entities;
using TrandingSystem.Domain.Interfaces;
using TrandingSystem.Infrastructure.Data;

namespace TrandingSystem.Infrastructure.Repositories
{
    public class CommunityRepository : ICommunityRepository
    {
        private readonly db23617Context _context;
        public CommunityRepository(db23617Context context)
        {
            _context = context;
        }

        public async Task<bool> IsUserInCommunityAsync(int communityId, int userId)
        {
            return await _context.CommunityMembers.AnyAsync(cm => cm.CommunityId == communityId && cm.UserId == userId);
        }

        public async Task<Community> GetByIdAsync(int communityId)
        {
            return await _context.Communities.Include(c => c.CommunityMembers).FirstOrDefaultAsync(c => c.CommunityId == communityId);
        }

        public async Task<List<Community>> GetForUserAsync(int userId)
        {
            return await _context.CommunityMembers
                .Where(cm => cm.UserId == userId)
                .Select(cm => cm.Community)
                .ToListAsync();
        }

        public async Task AddMemberAsync(int communityId, int userId)
        {
            if (!await IsUserInCommunityAsync(communityId, userId))
            {
                _context.CommunityMembers.Add(new CommunityMember { CommunityId = communityId, UserId = userId, JoinedAt = DateTime.UtcNow });
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveMemberAsync(int communityId, int userId)
        {
            var member = await _context.CommunityMembers.FirstOrDefaultAsync(cm => cm.CommunityId == communityId && cm.UserId == userId);
            if (member != null)
            {
                _context.CommunityMembers.Remove(member);
                await _context.SaveChangesAsync();
            }
        }

        public bool IsUserBlocked(int communityId, int userId)
        {
            return _context.CommunityMembers.Any(cm => cm.CommunityId == communityId && cm.UserId == userId && cm.IsBlocked == true);
        }
    }
}
