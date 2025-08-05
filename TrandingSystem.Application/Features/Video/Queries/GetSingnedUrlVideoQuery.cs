
using MediatR;
using TradingSystem.Application.Common.Response;

namespace TrandingSystem.Application.Features.Video.Queries
{
    public class GetSingnedUrlVideoQuery:IRequest<Response<string>>
    {
        public int VideoId { get; set; }
        public GetSingnedUrlVideoQuery(int videoId)
        {
            VideoId = videoId;
        }
    }
}
