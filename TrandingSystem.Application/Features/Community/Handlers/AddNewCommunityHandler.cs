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
        public AddNewCommunityHandler(IUnitOfWork unitofwork, IStringLocalizer<ValidationMessages> localizer)
        {
            _unitofwork = unitofwork;
            _localizer = localizer;
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
                    IsClosed=request.Community.IsClosed
                };
                // Add community to DbContext
                _unitofwork.Communities.Create(Community);

                // Save to DB
                await _unitofwork.SaveChangesAsync();

                // بعد الحفظ، EF Core هيعبي الـ Id من الـ DB
                var newCommunityId = Community.CommunityId;

                // Check Community is defualt or not 
                // Is IsDefualt Is True This Meaning the Community For All User In sysytem
                if (request.Community.IsDefault)
                {
                  
                    // Add CommunityMember For All User In System
                    foreach(var user in _unitofwork.Users.Read())
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
                    foreach (var user in course.CourseEnrollments)
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
