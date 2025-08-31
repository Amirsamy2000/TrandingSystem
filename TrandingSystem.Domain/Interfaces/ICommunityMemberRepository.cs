using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrandingSystem.Domain.Abstractions;
using TrandingSystem.Domain.Entities;

namespace TrandingSystem.Domain.Interfaces
{
    public interface ICommunityMemberRepository: IDomainInterface<CommunityMember>
    {
        List<CommunityMember>  GetMembersByUserids(List<int> ids, int communityid);
        void DeleteRange(List<CommunityMember> members);
    }
}
