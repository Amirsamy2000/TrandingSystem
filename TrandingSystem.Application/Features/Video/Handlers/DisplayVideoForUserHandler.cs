
using MediatR;
using Microsoft.Extensions.Localization;
using TradingSystem.Application.Common.Response;
 using TrandingSystem.Application.Features.Video.Queries;
using TrandingSystem.Application.Resources;
using TrandingSystem.Domain.Interfaces;

namespace TrandingSystem.Application.Features.Video.Handlers
{
    public class DisplayVideoForUserHandler : IRequestHandler<DisplayVideoForUserQuery, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private IFileService _fileService;
        private IStringLocalizer<ValidationMessages> _localizer;
        public DisplayVideoForUserHandler(IUnitOfWork unitOfWork, IFileService fileService, IStringLocalizer<ValidationMessages> localizer)
        {
            _unitOfWork = unitOfWork;
            _fileService = fileService;
            _localizer = localizer;
        }
        public async Task<Response<string>> Handle(DisplayVideoForUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // 1. check if video exists
                var video = _unitOfWork.Videos.ReadById(request.VideoId);
                if (video is null)
                    return Response<string>.ErrorResponse(_localizer["NotFoundVideo"]);

                // 2. check if video is active
                if (video.IsActive != true)
                    return Response<string>.ErrorResponse(_localizer["VideoIsBlock"]);

                // 3. if video is free → return directly
                if (video.IsPaid == false)
                {
                    var freeUrl = await _fileService.GetVideoUrlAsync(video.VideoUrl);
                    return Response<string>.SuccessResponse(freeUrl);
                }

                // 4. if video is paid → check enrollment
                var enrollment = _unitOfWork.ordersEnorllment
                    .CheckUserEnrollment(x => x.VideoId == request.VideoId && x.UserId == request.UserId);

                if (enrollment is null)
                    return Response<string>.ErrorResponse(_localizer["NotEnrollinthisvideo"]);

                // 5. enrolled → return paid video url
                var paidUrl = await _fileService.GetVideoUrlAsync(video.VideoUrl);
                return Response<string>.SuccessResponse(paidUrl);
            }
            catch
            {
                return Response<string>.ErrorResponse(_localizer["GeneralError"]);
            }
        }
    }
}
