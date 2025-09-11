


using MediatR;
using Microsoft.AspNetCore.Http;
using TradingSystem.Application.Common.Response;

namespace TrandingSystem.Application.Features.LiveSessions.Commands
{
    public class EnrollmentInPaidSessionCommand:IRequest<Response<bool>>
    {
        public int SessionId { get; set; }
        public IFormFile RecieptImage { get; set; }
        public int UserId { get; set; }
        
        public EnrollmentInPaidSessionCommand(int sessionId, IFormFile recieptImage, int userId)
        {
            SessionId = sessionId;
            RecieptImage = recieptImage;
            UserId = userId;
        }
    }
}
