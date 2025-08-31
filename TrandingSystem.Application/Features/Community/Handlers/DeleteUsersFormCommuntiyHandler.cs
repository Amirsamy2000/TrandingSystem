 
using MediatR;
using Microsoft.Extensions.Localization;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Features.Community.Commands;
using TrandingSystem.Application.Resources;
using TrandingSystem.Domain.Interfaces;

namespace TrandingSystem.Application.Features.Community.Handlers
{
    public class DeleteUsersFormCommuntiyHandler:IRequestHandler<DeleteUsersFormCommuntiyCommand,Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<ValidationMessages> _localizer;
        public DeleteUsersFormCommuntiyHandler(IStringLocalizer<ValidationMessages> localizer, IUnitOfWork unitOfWork)
        {
            _localizer = localizer;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<bool>> Handle(DeleteUsersFormCommuntiyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var membersincommunity = _unitOfWork.CommunityMember.GetMembersByUserids(request.UsersIds, request.CommunityId);
                if (membersincommunity is null || membersincommunity.Count == 0)
                {
                    return Response<bool>.ErrorResponse(_localizer["NotFountMembers"]);
                }
                _unitOfWork.CommunityMember.DeleteRange(membersincommunity);
               await _unitOfWork.SaveChangesAsync();

                return Response<bool>.SuccessResponse(true, _localizer["GeneralOperationDone"]);

            }
            catch
            {
                return Response<bool>.ErrorResponse(_localizer["GeneralServerFail"]);

            }
        }
    }
}
