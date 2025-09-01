using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Features.Courses.Commands;
using TrandingSystem.Application.Resources;
using TrandingSystem.Domain.Entities;
using TrandingSystem.Domain.Interfaces;
using TrandingSystem.Infrastructure.Constants;

namespace TrandingSystem.Application.Features.Courses.Handlers
{
    internal class AddCourseHandler : IRequestHandler<AddCourseCommand, Response<Course>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;
        private readonly IFileService _imageService;
        private readonly IStringLocalizer<ValidationMessages> _localizer;
        private readonly INotificationService _notificationService;



        public AddCourseHandler(IUnitOfWork unitOfWork, IMapper mapper, IFileService imageService,IStringLocalizer<ValidationMessages> localizer, INotificationService notificationService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _imageService = imageService;
            _localizer = localizer;
            _notificationService = notificationService;
        }

        public async Task<Response<Course>> Handle(AddCourseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var course = _mapper.Map<Course>(request);
                if (course == null)
                {
                    return Response<Course>.ErrorResponse("Invalid course data");
                }

                if(request.CourseImage != null)
                {
                    course.ImageCourseUrl = _imageService.SaveImageAsync(request.CourseImage, ConstantPath.PathdCoursesImages).Result;

                }

                // TODO : Add created by field

                var result = _unitOfWork.Courses.Create(course);

                if (result == null)
                {
                    return Response<Course>.ErrorResponse("Failed to create course");
                }

                // Add Community if CommunityAutoCreate is true
                bool autoCreate = request.CommunityAutoCreate ?? false;
                if (autoCreate)
                {
                    var community = new TrandingSystem.Domain.Entities.Community()
                    {
                        CreatedAt = DateTime.Now,
                        CourseId = result.CourseId,
                        Title = $"{request.TitleEN} Community",
                        IsAdminOnly = false,
                        IsClosed = false,
                        IsDefault = false,


                    };
                  
                    _unitOfWork.Communities.Create(community);
                   await _unitOfWork.SaveChangesAsync();
                }

                // notifaction 
                var Users = _unitOfWork.Users.Read();
                var EmailTemp = new Domain.Helper.EmailBody()
                {
                    dir = _localizer["dir"],
                    Subject = _localizer["stieName"],
                    Hi = _localizer["hi"],
                    info1 = _localizer["infocours1"],
                    info2 = _localizer["infoco2"] + " " + result.TitleEN,
                    info3 = "",
                    contact = _localizer["contact"],
                    namebtn = "Show Course",
                    ActionUrl = $"http://saifalqadi.runasp.net/Home/Courses"

                };

                _notificationService.SendMailForGroupUserAfterCreateBodey(Users, _localizer["FormalSub"], EmailTemp);
                return Response<Course>.SuccessResponse(result, "Course created successfully");
            }
            catch (Exception ex)
            {
                return Response<Course>.ErrorResponse($"An error occurred while processing the request: {ex.Message}");
            }
            
        }
    }
}
