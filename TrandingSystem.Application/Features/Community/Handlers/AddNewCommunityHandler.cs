using Amazon.Runtime.Internal;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Features.Community.Commands;
using TrandingSystem.Application.Features.Courses.Commands;
using TrandingSystem.Application.Resources;
using TrandingSystem.Domain.Entities;
using TrandingSystem.Domain.Interfaces;

namespace TrandingSystem.Application.Features.Community.Handlers
{
    public class AddNewCommunityHandler : IRequestHandler<AddNewCommunityCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IStringLocalizer<ValidationMessages> _localizer;
        private readonly INotificationService _notificationService;

        public AddNewCommunityHandler(IUnitOfWork unitofwork, IStringLocalizer<ValidationMessages> localizer, INotificationService notificationService)
        {
            _unitofwork = unitofwork;
            _localizer = localizer;
            _notificationService = notificationService;
        }
        public async Task<Response<bool>> Handle(AddNewCommunityCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Check if the community already exists
                var existingCommunity = _unitofwork.Communities.GetByTitle(request.Community.Title);
                if (existingCommunity) return Response<bool>.ErrorResponse(_localizer["CommunityAlreadyExists"]);
                var Community = new TrandingSystem.Domain.Entities.Community()
                {
                    IsDefault = request.Community.IsDefault,
                    IsAdminOnly=request.Community.IsAdminOnly,
                    Title=request.Community.Title,
                    CourseId= request.Community.IsDefault?null:request.Community.CourseId,
                    IsClosed=request.Community.IsClosed,
                    CreatedAt=DateTime.UtcNow
                };
                // Add community to DbContext
                _unitofwork.Communities.Create(Community);

                // Save to DB
                await _unitofwork.SaveChangesAsync();

                // بعد الحفظ، EF Core هيعبي الـ Id من الـ DB
                var newCommunityId = Community.CommunityId;

                // Check Community is defualt or not 
                // Is IsDefualt Is True This Meaning the Community For All User In sysytem

                // Notfiy 
                var EmailTemp = new Domain.Helper.EmailBody()
                {
                    dir = _localizer["dir"],
                    Subject = _localizer["stieName"],
                    StieName= _localizer["stieName"],
                    Hi = _localizer["hi"],
                    info1 = _localizer["infocomm1"],
                    info2 = _localizer["infocomm2"] + ": " + request.Community.Title,
                    info3 = "",
                    contact = _localizer["contact"],
                    namebtn = "Show Community",
                    ActionUrl = $"http://saifalqadi.runasp.net/Communities/ShowCommunitiesForUser"

                };
                 if (request.Community.IsDefault)
                {
                    var AllActiveUser = _unitofwork.Users.GetActiveAndConfirmUser();

                    // Add CommunityMember For All User In System
                    foreach (var user in AllActiveUser)
                    {
                        var communityMember = new CommunityMember()
                        {
                            UserId = user.Id,
                            IsBlocked = false,
                            JoinedAt = DateTime.UtcNow,
                            CommunityId = newCommunityId,

                        };
                        _unitofwork.CommunityMember.Create(communityMember);

                    }


                    await _unitofwork.SaveChangesAsync();
                  
                    _notificationService.SendMailForGroupUserAfterCreateBodey(AllActiveUser, _localizer["FormalSub"], EmailTemp);

                    return Response<bool>.SuccessResponse(true, _localizer["CommunityCreated"]);


                }
                // Else This Meaning the Community For Specific Users Enroll in  Course
                else
                {
                    // Add CommunityMember For All User Enroll in Course
                    var course = _unitofwork.Courses.ReadById(request.Community.CourseId.Value);
                    if (course == null)
                    {
                        return Response<bool>.ErrorResponse(_localizer["CourseNotFound"]);
                    }
                    var userincourse = course.CourseEnrollments.Where(x => x.OrderStatus == 1).Select(x=>x.User).ToList();
                    foreach (var user in course.CourseEnrollments.Where(x => x.OrderStatus == 1))
                    {
                        var communityMember = new CommunityMember()
                        {
                            UserId = user.UserId,
                            IsBlocked = false,
                            JoinedAt = DateTime.UtcNow,
                            CommunityId = newCommunityId,
                        };
                        _unitofwork.CommunityMember.Create(communityMember);
                    }
                    await _unitofwork.SaveChangesAsync();
                    _notificationService.SendMailForGroupUserAfterCreateBodey(userincourse, _localizer["FormalSub"], EmailTemp);

                    return Response<bool>.SuccessResponse(true, _localizer["CommunityCreated"]);


                }


            }
            catch
            {

                return Response<bool>.ErrorResponse( _localizer["GeneralError"]);


            }
        }
    }
}
