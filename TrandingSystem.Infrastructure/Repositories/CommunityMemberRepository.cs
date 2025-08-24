using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrandingSystem.Domain.Entities;
using TrandingSystem.Domain.Interfaces;
using TrandingSystem.Infrastructure.Data;

namespace TrandingSystem.Infrastructure.Repositories
{
    public class CommunityMemberRepository : ICommunityMemberRepository
    {
        private readonly db23617Context _context;
        public CommunityMemberRepository(db23617Context context)
        {
            _context = context;
        }
        public CommunityMember Create(CommunityMember Object)
        {
            _context.CommunityMembers.Add(Object);
            return Object;
        }

        public CommunityMember Delete(int Id)
        {
            var members = _context.CommunityMembers.Where(x => x.CommunityId == Id);
            _context.CommunityMembers.RemoveRange(members);
            return members.FirstOrDefault();
         }

        public List<CommunityMember> Read()
        {
            throw new NotImplementedException();
        }

        public CommunityMember ReadById(int Id)
        {
            throw new NotImplementedException();
        }

        public CommunityMember Update(CommunityMember Element)
        {
            throw new NotImplementedException();
        }
    }
}
