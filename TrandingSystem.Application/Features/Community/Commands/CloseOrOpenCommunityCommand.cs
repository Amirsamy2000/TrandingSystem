

using Amazon.Runtime.Internal;
using MediatR;
using TradingSystem.Application.Common.Response;

namespace TrandingSystem.Application.Features.Community.Commands
{
    public class CloseOrOpenCommunityCommand:IRequest<Response<bool>>
    {
        public int CommunityId { set; get; }
        public bool IsClose { set; get; }
        public CloseOrOpenCommunityCommand(int communityId, bool isClose)
        {
            CommunityId = communityId;
            IsClose = isClose;
        }
    }
}
