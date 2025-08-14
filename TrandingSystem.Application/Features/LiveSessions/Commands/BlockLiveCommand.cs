 using MediatR;
using TradingSystem.Application.Common.Response;

namespace TrandingSystem.Application.Features.LiveSessions.Commands
{
    public class BlockLiveCommand:IRequest<Response<bool>>
    {
        public int SessionId { get; set; }
        public bool Status { get; set; }
        public BlockLiveCommand(int sessionId, bool status)
        {
            SessionId = sessionId;
            Status = status;
        }
    }
}
