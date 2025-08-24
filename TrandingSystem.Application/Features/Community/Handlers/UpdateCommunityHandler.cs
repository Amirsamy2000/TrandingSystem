
using MediatR;
using Microsoft.Extensions.Localization;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Features.Community.Commands;
using TrandingSystem.Application.Resources;
using TrandingSystem.Domain.Interfaces;

namespace TrandingSystem.Application.Features.Community.Handlers
{
    public class UpdateCommunityHandler : IRequestHandler<UpdateCommunityCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public readonly IStringLocalizer<ValidationMessages> _localizer;
        public UpdateCommunityHandler(IStringLocalizer<ValidationMessages> localizer,IUnitOfWork unitOfWork)
        {
            _localizer = localizer;
            _unitOfWork = unitOfWork;
        }
        
        public async Task<Response<bool>> Handle(UpdateCommunityCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var community = _unitOfWork.Communities.ReadById(request.CommunityId);
                if (community is null)
                {
                    return Response<bool>.ErrorResponse(_localizer["NotFountCommunity"]);
                }

                community.Title = request.Title;
                _unitOfWork.Communities.Update(community);
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
