 
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Text;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Features.Users.Commands;
using TrandingSystem.Application.Resources;
using TrandingSystem.Domain.Entities;


namespace TrandingSystem.Application.Features.Users.Handlers
{
    public class EditUserHandler : IRequestHandler<EditUserCommand, Response<string>>
    {
        private readonly UserManager<User> _userManager;

        private readonly IStringLocalizer<ValidationMessages> _localizer;
        public EditUserHandler(UserManager<User> userManager, IStringLocalizer<ValidationMessages> localizer)
        {
            _userManager = userManager;
            _localizer = localizer;
        }
        public async Task<Response<string>> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            var dto = request.EditUserDto;

            var user = await _userManager.FindByIdAsync(dto.Id.ToString());
            if (user == null)
                return Response<string>.ErrorResponse("User Not Found");



            if (await _userManager.Users.AnyAsync(u => u.PhoneNumber == dto.PhoneNumber && u.Id != dto.Id))
                return Response<string>.ErrorResponse("Phone number already used");

            if (await _userManager.Users.AnyAsync(u => u.NationalId == dto.NationalId && u.Id != dto.Id))
                return Response<string>.ErrorResponse("National ID already used");

            

            // 2- update user
            user.FullName = dto.FullName;
            user.PhoneNumber = dto.PhoneNumber;
            user.NationalId = dto.NationalId;


            var result = await _userManager.UpdateAsync(user);


            if (result.Succeeded)
                return Response<string>.SuccessResponse(_localizer["Done"], user.EmailConfirmed.ToString());

            return Response<string>.ErrorResponse(_localizer["ErrorOccurred"]);


 
        }
    }
}
