using MediatR;
using TradingSystem.Application.Common.Response;

namespace TrandingSystem.Application.Features.Community.Commands
{
    public class UpdateCommunityCommand:IRequest<Response<bool>>
    {
        public int CommunityId { set; get; }
        public string Title { set; get; }
        public bool IsAdminOnly { set; get; }
        public UpdateCommunityCommand(string title, int communityId,bool isAdmin )
        {
            Title = title;
            CommunityId = communityId;
            IsAdminOnly = isAdmin;
        }
    }
}
