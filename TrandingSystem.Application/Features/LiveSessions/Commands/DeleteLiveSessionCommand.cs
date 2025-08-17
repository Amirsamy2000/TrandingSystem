using MediatR;

using TradingSystem.Application.Common.Response;

namespace TrandingSystem.Application.Features.LiveSessions.Commands
{
    public class DeleteLiveSessionCommand:IRequest<Response<bool>>
    {
        public int SessionId { get; set; }
        public DeleteLiveSessionCommand(int sessionId)
        {
            SessionId = sessionId;
        }
    }
}
