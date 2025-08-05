using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Features.Video.Commands;
using TrandingSystem.Domain.Interfaces;
using TrandingSystem.Domain.Entities;
using TrandingSystem.Application.Resources;

namespace TrandingSystem.Application.Features.Video.Handlers
{
    public class UpdateVideoHandler : IRequestHandler<UpdateVideoCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IStringLocalizer<ValidationMessages> _localizer;
        private readonly IFileService _imageService;
        private readonly IMapper _mapper;

        public UpdateVideoHandler(IUnitOfWork unitOfWork, IStringLocalizer<ValidationMessages> localizer, IFileService imageService,IMapper mapper)
        {
            _unitofwork = unitOfWork;
            _localizer = localizer;
            _imageService = imageService;
            _mapper = mapper;
        }
        public async  Task<Response<bool>> Handle(UpdateVideoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Check If Video Is Founded
                var video = _unitofwork.Videos.ReadById(request.viedoUpdateDto.VideoId);
                if(video is null)
                {
                    return Response<bool>.ErrorResponse(_localizer["NotFound"]);
                }

                video.TitleAR = request.viedoUpdateDto.TitleAR;
                video.TitleEN = request.viedoUpdateDto.TitleEN;
                video.DescriptionAR = request.viedoUpdateDto.DescriptionAR;
                video.DescriptionEN = request.viedoUpdateDto.DescriptionEN;
                video.IsPaid = request.viedoUpdateDto.IsPaid;
                video.Cost = request.viedoUpdateDto.Cost;
                video.IsActive = request.viedoUpdateDto.IsActive;
                



                if (request.viedoUpdateDto.ImageVideoUrl != null)
                {
                    // save new image then delete old image
                    string oldImageUrl = video.ImageVideoUrl;
                    string UpdatedUrlImageOfVideo = await _imageService.SaveImageAsync(request.viedoUpdateDto.ImageVideoUrl, oldImageUrl);
                   video.ImageVideoUrl = UpdatedUrlImageOfVideo;

                }
                _unitofwork.SaveChangesAsync();
                return Response<bool>.SuccessResponse(true, _localizer["UpdateVideoMessage"]);
            }
            catch {
                string errorMsg = request.Culture == "ar"
                     ? "حدث خطأ أثناء تعديل  الفيديو"
                     : "An error occurred while Updating the video";
                return Response<bool>.ErrorResponse(
                    _localizer["FailureUpdate"],
                    status: System.Net.HttpStatusCode.InternalServerError
                );

            }

        }
    }
}
