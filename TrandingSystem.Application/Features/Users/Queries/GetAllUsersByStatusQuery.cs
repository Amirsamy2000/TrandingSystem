


using MediatR;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Dtos;

namespace TrandingSystem.Application.Features.Users.Queries
{
    public class GetAllUsersByStatusQuery:IRequest<Response<List<UserDto>>>
    {
        // 0 Get All User 
        // 1 Get  Just Active User Not Is Block And Not Confirm Email
        //2 Get All User Block 
        // 3 Get All User Not Confirm Email
        // 4 Get All User Out Coummunity By CommunityId
        // 5 Get All User In Coummunity By CommunityId
        public int Status { get; set; }
        public int Id { get; set; } = 0;
        public GetAllUsersByStatusQuery(int status,int id=0)
        {
            Status = status;
            Id = id;
        }
    }
}
