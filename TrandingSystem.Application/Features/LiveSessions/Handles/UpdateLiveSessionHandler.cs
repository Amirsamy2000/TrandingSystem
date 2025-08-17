using MediatR;
using Microsoft.Extensions.Localization;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Features.LiveSessions.Commands;
using TrandingSystem.Application.Resources;
using TrandingSystem.Domain.Interfaces;
using TrandingSystem.Infrastructure.Constants;
using TrandingSystem.Infrastructure.Repositories;

namespace TrandingSystem.Application.Features.LiveSessions.Handles
{
    public class UpdateLiveSessionHandler : IRequestHandler<UpdateLiveSessionCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<ValidationMessages> _localizer;
        private readonly IFileService _imageService;

        public UpdateLiveSessionHandler(IUnitOfWork unitOfWork,IStringLocalizer<ValidationMessages>localizer,IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
            _imageService = fileService;
        }
        public async Task<Response<bool>> Handle(UpdateLiveSessionCommand request, CancellationToken cancellationToken)
        {
            try{
                var liveUpdate = _unitOfWork.LiveSessionRepositry.ReadById(request.liveSessionUpdateDto.SessionId);
                if (liveUpdate == null)
                {
                    return Response<bool>.ErrorResponse(_localizer["notFoundMessage"]);
                }
                liveUpdate.TitleAR = request.liveSessionUpdateDto.TitleAR;
                liveUpdate.TitleEN = request.liveSessionUpdateDto.TitleEN;
                liveUpdate.DescriptionAR = request.liveSessionUpdateDto.DescriptionAR;
                liveUpdate.DescriptionEN = request.liveSessionUpdateDto.DescriptionEN;
                liveUpdate.YoutubeLink = request.liveSessionUpdateDto.YoutubeLink;
                liveUpdate.ScheduledAt = request.liveSessionUpdateDto.ScheduledAt;
                liveUpdate.ScheduledTime = request.liveSessionUpdateDto.ScheduledTime;
                liveUpdate.IsLocked = request.liveSessionUpdateDto.IsLocked;
                liveUpdate.Cost = request.liveSessionUpdateDto.Cost;
                liveUpdate.IsActive = request.liveSessionUpdateDto.IsActive;
                if (request.liveSessionUpdateDto.ImageSessionUrl != null)
                {
                    // save new image then delete old image
                    string oldImageUrl = liveUpdate.ImageSessionUrl;
                    string updatedUrlImageOfLiveSession = await _imageService.SaveImageAsync(request.liveSessionUpdateDto.ImageSessionUrl, ConstantPath.PathVideoImage, oldImageUrl);
                    liveUpdate.ImageSessionUrl = updatedUrlImageOfLiveSession;
                }
                await _unitOfWork.SaveChangesAsync();
                return Response<bool>.SuccessResponse(true, _localizer["GeneralOperationDone"]);

            }

            catch
            {
                return Response<bool>.ErrorResponse(_localizer["GeneralError"]);
            }


        }
    }
}
