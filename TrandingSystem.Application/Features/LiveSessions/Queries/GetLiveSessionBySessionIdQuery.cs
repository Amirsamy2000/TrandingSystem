using MediatR;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Dtos;

namespace TrandingSystem.Application.Features.LiveSessions.Queries
{
    public class GetLiveSessionBySessionIdQuery:IRequest<Response<LiveSessionAddDto>>
    {
        public int SessionId { get; set; }
        public GetLiveSessionBySessionIdQuery(int sessionId)
        {
            SessionId = sessionId;
        }   
    }
}
