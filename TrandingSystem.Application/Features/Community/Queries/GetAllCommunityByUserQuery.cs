
using MediatR;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Dtos;

namespace TrandingSystem.Application.Features.Community.Queries
{
    public class GetAllCommunityByUserQuery : IRequest<Response<List<CommunitiesDto>>>
    {

        public int UserId { get; set; }
        public GetAllCommunityByUserQuery(int userId)
        {
            UserId = userId;
        }
    }
}
