 
using MediatR;
using TradingSystem.Application.Common.Response;


namespace TrandingSystem.Application.Features.Community.Commands
{
    public class BlockOrActiveUserCommand:IRequest<Response<bool>>
    {
        public List<int> Userids { get; set; }
        public bool IsBlock { get; set; }
        public int CommunityId { set; get; }

        public BlockOrActiveUserCommand(List<int> userids, bool isBlock, int communityId)
        {
            Userids = userids;
            IsBlock = isBlock;
            CommunityId = communityId;
        }
    }
}
