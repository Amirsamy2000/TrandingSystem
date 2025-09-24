using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Localization;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Dtos;
using TrandingSystem.Application.Features.Video.Queries;
using TrandingSystem.Application.Resources;
using TrandingSystem.Domain.Entities;
using TrandingSystem.Domain.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TrandingSystem.Application.Features.Video.Handlers
{
    public class GetLivesAndVideosByUserEnrollmentHandler : IRequestHandler<GetLivesAndVideosByUserEnrollmentQuery, Response<LivesAndVideosDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<ValidationMessages> _localizer;

        public GetLivesAndVideosByUserEnrollmentHandler(IUnitOfWork unitOfWork, IMapper mapper, IStringLocalizer<ValidationMessages> localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public  async Task<Response<LivesAndVideosDto>> Handle(GetLivesAndVideosByUserEnrollmentQuery request, CancellationToken cancellationToken)
        {

            try
            {
                // check if user enroll in course
                var checkEnrollmentUser = _unitOfWork.ordersEnorllment.CheckUserEnrollment(x => x.CourseId == request.CourseId && x.UserId == request.UserId&& x.OrderStatus==1);
                var course = _unitOfWork.Courses.ReadById(request.CourseId);
                // if found enrollment 
                if (checkEnrollmentUser is not null)
                {
                    // 0 get all videos and lives for this course
                    // 1 get  Just all Active Videos
                    // 2 get just all unActive Videos
                    var videos = _unitOfWork.Videos.GetAllVideosForCouse(request.CourseId,1);

                    // 0 get all lives for this course
                    // 1 get  Just all Active Lives
                    // 2 get just all unActive Lives
                    var Lives = _unitOfWork.LiveSessionRepositry.GetAllLiveSessionsForCouse(request.CourseId,1);
                    var resutVideosDto = _mapper.Map<List<VideoDto>>(videos, opt =>
                    {
                        opt.Items["culture"] = request.Culture;
                        

                    });

                    var resultLiveSessionsDto = _mapper.Map<List<LiveSessionsDto>>(Lives, opt =>
                    {
                        opt.Items["culture"] = request.Culture;
                        
                    });

                    // Apply business rules for access
                    foreach (var video in resutVideosDto)
                    {
                        var checkEnrollmentVideoUser = _unitOfWork.ordersEnorllment.CheckUserEnrollment(x => x.VideoId == video.VideoId && x.UserId == request.UserId&& x.OrderStatus!=0 && x.Video.IsActive==true);

                        if (!video.IsPaid)
                        {  // مجاني
                            video.HassAccess = true; video.StatusOrder = 1;
                        }

                        else if (video.IsPaid && checkEnrollmentVideoUser is not null && checkEnrollmentVideoUser.OrderStatus == 1)
                        {  // مدفوع + مسجل
                            video.HassAccess = true; video.StatusOrder = 1;
                        }
                        else if (video.IsPaid && checkEnrollmentVideoUser is not null && checkEnrollmentVideoUser.OrderStatus == 2)
                        {  // مدفوع + الطلب معلق
                            video.HassAccess = true; video.StatusOrder = 0;
                        }
                        else
                        {  // مدفوع + مش مسجل
                            video.HassAccess = false;
                            video.StatusOrder = 2;
                        }
                    }

                    foreach (var live in resultLiveSessionsDto)
                    {
                        var checkEnrollmentLiveUser = _unitOfWork.ordersEnorllment.CheckUserEnrollment(x => x.liveId == live.SessionId && x.UserId == request.UserId && x.OrderStatus != 0 && x.Live.IsLocked==false);


                        if (live.IsLocked!=true)
                        {  // مجاني
                            live.HassAccess = true; live.StatusOrder = 1;
                        }
                        else if (live.IsLocked == true && checkEnrollmentLiveUser is not null && checkEnrollmentLiveUser.OrderStatus == 1)
                        {  // مدفوع + مسجل
                            live.HassAccess = true; live.StatusOrder = 1;
                        }

                        else if (live.IsLocked == true && checkEnrollmentLiveUser is not null && checkEnrollmentLiveUser.OrderStatus ==2)
                        {  // مدفوع + الطلب معلق
                            live.HassAccess = true; live.StatusOrder = 0;
                        }
                        else
                        {  // مدفوع + مش مسجل
                            live.HassAccess = false;
                            live.StatusOrder = 2;
                        }
                    }

                    var livesandvideos = new LivesAndVideosDto()
                    {
                        Lives = resultLiveSessionsDto,
                        Videos = resutVideosDto

                    };

                    string courseTitle = request.Culture == "ar" ? course.TitleAR : course.TitleEN;
                    return Response<LivesAndVideosDto>.SuccessResponse(livesandvideos, courseTitle);
                }
                else
                {
                    return Response<LivesAndVideosDto>.ErrorResponse(_localizer["NotEnrollinthiscourse"]);

                }

            }
            catch
            {
                return Response<LivesAndVideosDto>.ErrorResponse(_localizer["GeneralError"]);

            }


        }
    }
}
