using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrandingSystem.Domain.Abstractions;
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

        public Community Create(Community Object)
        {
            _context.Communities.Add(Object);
            return Object;
        }

        public Community Delete(int Id)
        {
            var community = ReadById(Id);
            _context.Communities.Remove(community);
            return community;
        }

        public IEnumerable<Community> GetAllCommunitiesByCourseId(int courseId)
        {
            // This method should return all communities by courseId
            // If courseId is 0, it should return all communities
            // If courseId is -1, it should return default communities
            // else  return communities by courseId
            if (courseId == 0)
            {
                return _context.Communities;
        }
            else if (courseId == -1)
        {
                return _context.Communities.Where(c => c.IsDefault == true);
            }
            else
            {
                return _context.Communities.Where(c => c.CourseId == courseId);
            }
            }

        public bool GetByTitle(string title)
        {
           return _context.Communities.Any(c => c.Title.ToLower()==title.ToLower());
        }

        public List<Community> Read()
        {
            throw new NotImplementedException();
        }

        public Community ReadById(int Id)
            {
           return _context.Communities.Find(Id);
            }

        public Community Update(Community Element)
        {
            _context.Communities.Update(Element);
            return Element;
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
    }
}
