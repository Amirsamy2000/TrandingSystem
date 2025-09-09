using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Dtos;
using TrandingSystem.Application.Features.Video.Queries;
using TrandingSystem.Application.Resources;
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
                var checkEnrollmentUser = _unitOfWork.ordersEnorllment.CheckUserEnrollment(x => x.CourseId == request.CourseId && x.UserId == request.UserId);
                var course = _unitOfWork.Courses.ReadById(request.CourseId);
                // if found enrollment 
                if (checkEnrollmentUser is not null)
                {
                    var videos = _unitOfWork.Videos.GetAllVideosForCouse(request.CourseId);
                    
                    var Lives = _unitOfWork.LiveSessionRepositry.GetAllLiveSessionsForCouse(request.CourseId);
                    var resutVideosDto = _mapper.Map<List<VideoDto>>(videos, opt =>
                    {
                        opt.Items["culture"] = request.Culture;
                    });

                    var resultLiveSessionsDto = _mapper.Map<List<LiveSessionsDto>>(Lives, opt =>
                    {
                        opt.Items["culture"] = request.Culture;
                    });

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
