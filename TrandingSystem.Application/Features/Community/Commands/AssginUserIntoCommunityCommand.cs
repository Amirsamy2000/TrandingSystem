using Amazon.Runtime.Internal;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.Application.Common.Response;

namespace TrandingSystem.Application.Features.Community.Commands
{
    public class AssginUserIntoCommunityCommand:IRequest<Response<bool>>
    {
        public int CommunityId { get; set; }
        public List<int> UserId { get; set; }

        public AssginUserIntoCommunityCommand(int communityId, List<int> userId)
        {
            CommunityId = communityId;
            UserId = userId;
        }
    }

}
