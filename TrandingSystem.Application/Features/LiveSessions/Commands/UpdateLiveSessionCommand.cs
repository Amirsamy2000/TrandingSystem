using MediatR;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Dtos;

namespace TrandingSystem.Application.Features.LiveSessions.Commands
{
    public class UpdateLiveSessionCommand:IRequest<Response<bool>>
    {
        public LiveSessionAddDto liveSessionUpdateDto { set;get; }  
        public UpdateLiveSessionCommand(LiveSessionAddDto liveSessionUpdateDto )
        {
            this.liveSessionUpdateDto = liveSessionUpdateDto;
        }
    }
}
