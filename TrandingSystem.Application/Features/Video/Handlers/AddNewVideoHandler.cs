 
using MediatR;
using Microsoft.Extensions.Localization;
 
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Features.Video.Commands;
using TrandingSystem.Application.Resources;
using TrandingSystem.Domain.Interfaces;
using TrandingSystem.Domain.Entities;
using AutoMapper;
using TrandingSystem.Infrastructure.Constants;
using TrandingSystem.Infrastructure.Services;
using TrandingSystem.Domain.Helper;


namespace TrandingSystem.Application.Features.Video.Handlers
{
    public class AddNewVideoHandler : IRequestHandler<AddNewVideoCommand, Response<bool>>
    {
      private readonly IUnitOfWork _unitOfWork;
      private readonly IFileService _imageService;
     private readonly IStringLocalizer<ValidationMessages> _localizer;
        private readonly INotificationService _notificationService;

        public AddNewVideoHandler(IUnitOfWork unitOfWork, IFileService imageService ,IStringLocalizer<ValidationMessages>localizer, INotificationService notificationService)
        {
            _unitOfWork = unitOfWork;
            _imageService = imageService;
           _localizer = localizer;
         _notificationService = notificationService;

        }
        public async Task<Response<bool>> Handle(AddNewVideoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Save If Image video
                string imageUrl=  _imageService.SaveImageAsync(request.VideoAddedDto.ImageVideoUrl, ConstantPath.PathVideoImage).Result;
                if (string.IsNullOrEmpty(imageUrl))
                {
                    return Response<bool>.ErrorResponse(_localizer["AddImageFail"]);
                }
                // Upload Video to Bunnay Server
                //string videoUrl = await _imageService.SaveVideoAsync(request.VideoAddedDto.VideoUrl);
                //if (string.IsNullOrEmpty(videoUrl))
                //{
                //    return Response<bool>.ErrorResponse(_localizer["AddVideofaild"]);

                //}

                var newVideo = new TrandingSystem.Domain.Entities.Video
                {
                    TitleAR =request.VideoAddedDto.TitleAR,
                    TitleEN = request.VideoAddedDto.TitleEN,
                    DescriptionAR = request.VideoAddedDto.DescriptionAR,
                    DescriptionEN = request.VideoAddedDto.DescriptionEN,
                    CourseId = request.VideoAddedDto.CourseId,
                    Cost = request.VideoAddedDto.Cost,
                    IsActive = request.VideoAddedDto.IsActive,
                    IsPaid = request.VideoAddedDto.IsPaid,
                    ImageVideoUrl = imageUrl,
                    VideoUrl = request.VideoAddedDto.VideoUrl,
                    CreadteBy =request.UserId,
                    CreatedAt=DateTime.Now,
                };

                // Add Video to Database
                _unitOfWork.Videos.Create(newVideo);
                await _unitOfWork.SaveChangesAsync();
                
                if (request.VideoAddedDto.notfiy)
                {
                    
                    // notify
                    var EmailTemp =new Domain.Helper.EmailBody()
                    {
                        dir = _localizer["dir"],
                        Subject = _localizer["stieName"],
                        Hi = _localizer["hi"],
                        info1 = _localizer["infoVido1"],
                        info2 = _localizer["infoVido2"] + " " + newVideo.TitleEN,
                        info3 = _localizer["infoVido3"] + " " + newVideo.Course.TitleEN,
                        contact = _localizer["contact"],
                        namebtn= _localizer["btnAddVideo"],
                        ActionUrl = $"http://saifalqadi.runasp.net/Home/GoToCourse?CourseId={newVideo.CourseId}"

                    };
                    var Users = _unitOfWork.Users.GetUserEnrollInCourse(newVideo.CourseId);
                    _notificationService.SendMailForGroupUserAfterCreateBodey(Users, _localizer["FormalSub"], EmailTemp);
                   
                }


                return Response<bool>.SuccessResponse(true, _localizer["AddVideoSuccess"]);
                // bre



            }
            catch
            {
                return Response<bool>.SuccessResponse(true, _localizer["AddnewVideoFail"]);

            }

        }
    }
}
