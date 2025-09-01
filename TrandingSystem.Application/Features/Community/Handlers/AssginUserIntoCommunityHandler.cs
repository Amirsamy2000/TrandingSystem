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
using TrandingSystem.Infrastructure.Services;

namespace TrandingSystem.Application.Features.Community.Handlers
{
    public class AssginUserIntoCommunityHandler : IRequestHandler<AssginUserIntoCommunityCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public readonly IStringLocalizer<ValidationMessages> _localizer;

        private readonly INotificationService _notificationService;

        public AssginUserIntoCommunityHandler(IStringLocalizer<ValidationMessages> localizer, IUnitOfWork unitOfWork, INotificationService notificationService)
        {
            _localizer = localizer;
            _unitOfWork = unitOfWork;
            _notificationService = notificationService;

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
                var EmailTemp = new Domain.Helper.EmailBody()
                {
                    dir = _localizer["dir"],
                    Subject = _localizer["stieName"],
                    StieName = _localizer["stieName"],
                    Hi = _localizer["hi"],
                    info1 = _localizer["infocomm1"],
                    info2 = _localizer["infocomm2"] + ": " + community.Title,
                    info3 = "",
                    contact = _localizer["contact"],
                    namebtn = "Show Community",
                    ActionUrl = $"http://saifalqadi.runasp.net/Communities/ShowCommunitiesForUser"

                };

                var Users = _unitOfWork.Users.Read().Where(x => request.UserId.Contains(x.Id)).ToList();
                foreach (var user in Users)
                {
                    EmailTemp.UserName = user.FullName;
                  

                    var member= new Domain.Entities.CommunityMember()
                    {
                        CommunityId = request.CommunityId,
                        UserId = user.Id,
                        IsBlocked = false,
                        JoinedAt = DateTime.Now
                    };

                    _unitOfWork.CommunityMember.Create(member);
                    _notificationService.SendMailForUserAfterCreateBodey(user.Email, _localizer["FormalSub"], EmailTemp);

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
