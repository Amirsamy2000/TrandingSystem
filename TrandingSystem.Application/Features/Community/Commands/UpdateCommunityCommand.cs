using MediatR;
using TradingSystem.Application.Common.Response;

namespace TrandingSystem.Application.Features.Community.Commands
{
    public class UpdateCommunityCommand:IRequest<Response<bool>>
    {
        public int CommunityId { set; get; }
        public string Title { set; get; }
        public UpdateCommunityCommand(string title, int communityId)
        {
            Title = title;
            CommunityId = communityId;
        }
    }
}
