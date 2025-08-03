 
using MediatR;
using Microsoft.Extensions.Localization;
 
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Features.Video.Commands;
using TrandingSystem.Application.Resources;
using TrandingSystem.Domain.Interfaces;
using TrandingSystem.Domain.Entities;
using AutoMapper;


namespace TrandingSystem.Application.Features.Video.Handlers
{
    public class AddNewVideoHandler : IRequestHandler<AddNewVideoCommand, Response<bool>>
    {
      private readonly IUnitOfWork _unitOfWork;
      private readonly IImageService _imageService;
     private readonly IStringLocalizer<ValidationMessages> _localizer;
     private readonly IMapper _mapper;

        public AddNewVideoHandler(IUnitOfWork unitOfWork, IImageService imageService ,IStringLocalizer<ValidationMessages>localizer,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _imageService = imageService;
           _localizer = localizer;
            _mapper = mapper;

        }
        public async Task<Response<bool>> Handle(AddNewVideoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Save If Image video
                string imageUrl=  _imageService.SaveVideoAsync(request.VideoAddedDto.ImageVideoUrl).Result;
                if (string.IsNullOrEmpty(imageUrl))
                {
                    return Response<bool>.ErrorResponse(_localizer["AddImageFail"]);
                }
                // Upload Video to Bunnay Server
                string videoUrl = await _imageService.SaveVideoAsync(request.VideoAddedDto.VideoUrl);
                if (string.IsNullOrEmpty(videoUrl))
                {
                    return Response<bool>.ErrorResponse(_localizer["AddVideofaild"]);

                }
               
                var newVideo= new TrandingSystem.Domain.Entities.Video
                {
                    TitleAR=request.VideoAddedDto.TitleAR,
                    TitleEN = request.VideoAddedDto.TitleEN,
                    DescriptionAR = request.VideoAddedDto.DescriptionAR,
                    DescriptionEN = request.VideoAddedDto.DescriptionEN,
                    CourseId = request.VideoAddedDto.CourseId,
                    Cost = request.VideoAddedDto.Cost,
                    IsActive = request.VideoAddedDto.IsActive,
                    IsPaid = request.VideoAddedDto.IsPaid,
                    ImageVideoUrl = imageUrl,
                    CreadteBy=request.UserId,
                    CreatedAt=DateTime.Now,


                };
                // Add Video to Database
                _unitOfWork.Videos.Create(newVideo);
                await _unitOfWork.SaveChangesAsync();
                return Response<bool>.SuccessResponse(true, _localizer["AddVideoSuccess"]);




            }
            catch
            {
                return Response<bool>.SuccessResponse(true, _localizer["AddnewVideoFail"]);

            }

        }
    }
}
