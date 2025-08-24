using Amazon.Runtime.Internal;
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
    public class DeleteCommunityHandler : IRequestHandler<DeleteCommunityCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<ValidationMessages> _localizer;
        public DeleteCommunityHandler(IStringLocalizer<ValidationMessages> localizer, IUnitOfWork unitOfWork)
        {
            _localizer = localizer;
            _unitOfWork = unitOfWork;
        }
        public async Task<Response<bool>> Handle(DeleteCommunityCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var community = _unitOfWork.Communities.ReadById(request.CommunityId);
                if (community is null)
                {
                    return Response<bool>.ErrorResponse(_localizer["NotFountCommunity"]);
                }
                _unitOfWork.CommunityMember.Delete(request.CommunityId);
                _unitOfWork.Communities.Delete(request.CommunityId);
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
