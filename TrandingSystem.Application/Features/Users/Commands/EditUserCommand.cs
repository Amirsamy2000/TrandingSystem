 
using MediatR;
 
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Dtos;

namespace TrandingSystem.Application.Features.Users.Commands
{
    public class EditUserCommand:IRequest<Response<string>>
    {
        public EditUserDto EditUserDto { get; set; }
        public EditUserCommand(EditUserDto editUserDto)
        {
            EditUserDto = editUserDto;
        }

    }
}
