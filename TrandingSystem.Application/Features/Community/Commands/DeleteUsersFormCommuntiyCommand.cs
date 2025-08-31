 
using MediatR;
using TradingSystem.Application.Common.Response;


namespace TrandingSystem.Application.Features.Community.Commands
{
    public class DeleteUsersFormCommuntiyCommand:IRequest<Response<bool>>
    {
        public List<int> UsersIds { get; set; }
        public int CommunityId { get; set; }
        public DeleteUsersFormCommuntiyCommand(List<int> userids,int communityid)
        {
            UsersIds = userids;
            CommunityId = communityid;
        }
    }
}
