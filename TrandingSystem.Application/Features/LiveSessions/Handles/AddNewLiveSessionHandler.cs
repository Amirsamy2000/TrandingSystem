using Amazon.Runtime.Internal;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Features.LiveSessions.Commands;
using TrandingSystem.Application.Resources;
using TrandingSystem.Domain.Interfaces;
using TrandingSystem.Infrastructure.Constants;
using TrandingSystem.Infrastructure.Services;

namespace TrandingSystem.Application.Features.LiveSessions.Handles
{
    public class AddNewLiveSessionHandler : IRequestHandler<AddNewLiveSessionCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _imageService;
        private readonly IStringLocalizer<ValidationMessages> _localizer;
        private readonly INotificationService _notificationService;

        public AddNewLiveSessionHandler(IUnitOfWork unitOfWork, IFileService imageService, IStringLocalizer<ValidationMessages> localizer, INotificationService notificationService)
        {
            _unitOfWork = unitOfWork;
            _imageService = imageService;
            _localizer = localizer;
            _notificationService = notificationService;
        }
        public async Task<Response<bool>> Handle(AddNewLiveSessionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                string imageUrl = _imageService.SaveImageAsync(request.LiveSessionAdd.ImageSessionUrl, ConstantPath.PathVideoImage).Result;
                if (string.IsNullOrEmpty(imageUrl))
                {
                    return Response<bool>.ErrorResponse(_localizer["AddImageFail"]);
                }
                var newLiveSession = new TrandingSystem.Domain.Entities.LiveSession
                {
                    TitleAR = request.LiveSessionAdd.TitleAR,
                    TitleEN = request.LiveSessionAdd.TitleEN,
                    DescriptionAR = request.LiveSessionAdd.DescriptionAR,
                    DescriptionEN = request.LiveSessionAdd.DescriptionEN,
                    CourseId = request.LiveSessionAdd.CourseId,
                    Cost = request.LiveSessionAdd.Cost,
                    IsActive = request.LiveSessionAdd.IsActive,
                    IsLocked = request.LiveSessionAdd.IsLocked,
                    ImageSessionUrl = imageUrl,
                    ScheduledTime = request.LiveSessionAdd.ScheduledTime,
                    ScheduledAt = request.LiveSessionAdd.ScheduledAt,
                    CreadteBy = request.UserId,
                    YoutubeLink=request.LiveSessionAdd.YoutubeLink
                };
                 _unitOfWork.LiveSessionRepositry.Create(newLiveSession);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                
                if (request.LiveSessionAdd.notfiy)
                {
                    // notify
                    var EmailTemp = new  Domain.Helper.EmailBody()
                    {
                        dir = _localizer["dir"],
                        Subject = _localizer["stieName"],
                        Hi = _localizer["hi"],
                        info1 = _localizer["infolive1"] + " <br> <br>" + newLiveSession.TitleEN,
                        info2 = _localizer["infoLive2"]+": "+ newLiveSession.ScheduledAt.ToString("dd-MM-yyyy")
                        + "<br><br>"+ _localizer["infoLivetime2"] +": " + newLiveSession.ScheduledTime,
                        info3 = _localizer["infoVido3"] + " " + newLiveSession.Course.TitleEN,
                        contact = _localizer["contact"],
                        ActionUrl = $"https://penalin897-001-site1.stempurl.com/Home/GoToCourse?CourseId={newLiveSession.CourseId}"

                    };

                    var Users = _unitOfWork.Users.GetUserEnrollInCourse(newLiveSession.CourseId);
                    _notificationService.SendMailForGroupUserAfterCreateBodey(Users, _localizer["FormalSub"], EmailTemp);
                }
                
                return Response<bool>.SuccessResponse(true, _localizer["AddLiveSessionSuccess"]);


            }
            catch 
            {
                return Response<bool>.ErrorResponse(_localizer["GeneralServerFail"]);
            }

        }


    }
}
