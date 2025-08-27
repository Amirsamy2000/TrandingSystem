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
    public class AssginUserIntoCommunityHandler : IRequestHandler<AssginUserIntoCommunityCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public readonly IStringLocalizer<ValidationMessages> _localizer;


        public AssginUserIntoCommunityHandler(IStringLocalizer<ValidationMessages> localizer, IUnitOfWork unitOfWork)
        {
            _localizer = localizer;
            _unitOfWork = unitOfWork;
        }
        public async Task<Response<bool>> Handle(AssginUserIntoCommunityCommand request, CancellationToken cancellationToken)
        {
            try
            { 
                // Check Found Community
                var community = _unitOfWork.Communities.ReadById(request.CommunityId);
                if (community is null)
                {
                    return Response<bool>.ErrorResponse(_localizer["NotFountCommunity"]);
                }
                foreach(var user in request.UserId)
                {
                    var member= new Domain.Entities.CommunityMember()
                    {
                        CommunityId = request.CommunityId,
                        UserId = user,
                        IsBlocked = false,
                        JoinedAt = DateTime.Now
                    };
                    _unitOfWork.CommunityMember.Create(member);
                }
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
