 
using MediatR;
using TradingSystem.Application.Common.Response;


namespace TrandingSystem.Application.Features.Video.Queries
{
    public class DisplayVideoForUserQuery : IRequest<Response<string>>
    {
        public int  VideoId { get; set; }
        public int UserId { get; set; }

        public DisplayVideoForUserQuery(int videoId, int userId)
        {
            VideoId = videoId;
            UserId = userId;
        }
    }
}
