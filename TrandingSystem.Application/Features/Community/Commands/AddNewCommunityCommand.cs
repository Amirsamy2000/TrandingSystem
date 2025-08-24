using MediatR;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Dtos;

namespace TrandingSystem.Application.Features.Community.Commands
{
    public class AddNewCommunityCommand : IRequest<Response<bool>>
    {
        public CommunityCreateDto Community { get; set; }
        public AddNewCommunityCommand(CommunityCreateDto community)
        {
            Community = community;
        }
    }
}