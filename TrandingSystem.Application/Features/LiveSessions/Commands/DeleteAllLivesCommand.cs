
using MediatR;
using TradingSystem.Application.Common.Response;

namespace TrandingSystem.Application.Features.LiveSessions.Commands
{
    public class DeleteAllLivesCommand:IRequest<Response<bool>>
    {
        public int CourselId { get; set; }

        public DeleteAllLivesCommand(int courselId)
        {
            CourselId = courselId;
        }
    }
}
