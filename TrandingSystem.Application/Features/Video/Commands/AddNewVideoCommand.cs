
using MediatR;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Dtos;

namespace TrandingSystem.Application.Features.Video.Commands
{
    public class AddNewVideoCommand:IRequest<Response<bool>>
    {
        public VideoAddedDto VideoAddedDto { get; set; }
        public int UserId { get; set; } 
        public AddNewVideoCommand(VideoAddedDto videoAddedDto, int userId)
        {
            VideoAddedDto = videoAddedDto;
            UserId = userId;
        }
    }
}
