using Amazon.Runtime.Internal;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Dtos;

namespace TrandingSystem.Application.Features.LiveSessions.Commands
{
    public class AddNewLiveSessionCommand:IRequest<Response<bool>>
    {
        public int UserId { get; set; }
        public LiveSessionAddDto LiveSessionAdd{set; get; }
        public AddNewLiveSessionCommand(int userId, LiveSessionAddDto liveSessionAdd)
        {
            UserId = userId;
           LiveSessionAdd = liveSessionAdd;
        }
    }

}
