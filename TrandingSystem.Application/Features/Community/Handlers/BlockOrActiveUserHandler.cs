using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Features.Community.Commands;
using TrandingSystem.Application.Resources;
using TrandingSystem.Domain.Interfaces;

namespace TrandingSystem.Application.Features.Community.Handlers
{
    public class BlockOrActiveUserHandler : IRequestHandler<BlockOrActiveUserCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<ValidationMessages> _localizer;
        public BlockOrActiveUserHandler(IStringLocalizer<ValidationMessages> localizer, IUnitOfWork unitOfWork)
        {
            _localizer = localizer;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<bool>> Handle(BlockOrActiveUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var membersincommunity = _unitOfWork.CommunityMember.GetMembersByUserids(request.Userids, request.CommunityId);
                foreach (var member in membersincommunity)
                {
                    member.IsBlocked = request.IsBlock;
                    _unitOfWork.CommunityMember.Update(member);
                    await _unitOfWork.SaveChangesAsync();
                }

                return Response<bool>.SuccessResponse(true, _localizer["GeneralOperationDone"]);

            }
            catch
            {
                return Response<bool>.ErrorResponse(_localizer["GeneralServerFail"]);

            }
        }
    }
}
