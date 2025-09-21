using AutoMapper;
using MediatR;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Dtos;
using TrandingSystem.Application.Features.LiveSessions.Queries;
using TrandingSystem.Domain.Interfaces;

namespace TrandingSystem.Application.Features.LiveSessions.Handles
{

    public class GetLiveSessionsByCourseIdHandler: IRequestHandler<GetLiveSessionsByCourseIdQuery, Response<IEnumerable<LiveSessionsDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetLiveSessionsByCourseIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<LiveSessionsDto>>> Handle(GetLiveSessionsByCourseIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var course = _unitOfWork.Courses.ReadById(request.CourseId);
                string courseName = request.Culture == "ar" ? course.TitleAR : course.TitleEN;
               var Query=  _unitOfWork.LiveSessionRepositry.GetAllLiveSessionsForCouse(request.CourseId,0);
                // هنا تستخدم AutoMapper بشكل عادي بعد ما تجيب البيانات من قاعدة البيانات
                var result = _mapper.Map<List<LiveSessionsDto>>(Query, opt =>
                {
                    opt.Items["culture"] = request.Culture;
                });
                result = result.Select(x =>
                {
                    x.CourseName = courseName;
                    x.CourseId = course.CourseId;
                    return x;
                }).ToList();

                // 4) رجع Response موحد
                return Response <IEnumerable<LiveSessionsDto>>
                    .SuccessResponse(result, courseName);

            }
            catch (Exception ex)
            {
                string errorMessage =request.Culture == "ar" ? "حدث خطأ أثناء جلب الجلسات المباشرة":
                    "Error while fetching live sessions";
                return Response<IEnumerable<LiveSessionsDto>>.ErrorResponse(
                    errorMessage
                    );
            }
        }
    }
}
