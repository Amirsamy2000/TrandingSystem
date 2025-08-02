using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Features.Video.Commands;
using TrandingSystem.Domain.Interfaces;
using TrandingSystem.Domain.Entities;

namespace TrandingSystem.Application.Features.Video.Handlers
{
    public class UpdateVideoHandler : IRequestHandler<UpdateVideoCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IStringLocalizer<UpdateVideoHandler> _localizer;
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;

        public UpdateVideoHandler(IUnitOfWork unitOfWork, IStringLocalizer<UpdateVideoHandler> localizer, IImageService imageService,IMapper mapper)
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

                var UpdatedVideo= _mapper.Map(request.viedoUpdateDto, video);
                UpdatedVideo.VideoUrl = video.VideoUrl;
                UpdatedVideo.ImageVideoUrl = video.ImageVideoUrl;
                // Ckeck Addmin Update Image Or Not 


                if (request.viedoUpdateDto.ImageVideoUrl != null)
                {
                    // save new image then delete old image
                    string UpdatedUrlImageOfVideo = await _imageService.SaveImageAsync(request.viedoUpdateDto.ImageVideoUrl);
                    UpdatedVideo.ImageVideoUrl = UpdatedUrlImageOfVideo;

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
