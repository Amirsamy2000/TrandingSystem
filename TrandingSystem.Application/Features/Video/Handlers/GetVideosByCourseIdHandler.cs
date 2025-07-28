using AutoMapper;
using MediatR;
using System.Net;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Dtos;
using TrandingSystem.Application.Features.Video.Queries;
using TrandingSystem.Domain.Entities;
using TrandingSystem.Domain.Interfaces;
 

namespace TrandingSystem.Application.Features.Video.Handlers
{
    public class GetVideosByCourseIdHandler : IRequestHandler<GetVideosByCourseIdQuery, Response<IEnumerable<VideoDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetVideosByCourseIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async  Task<Response<IEnumerable<VideoDto>>> Handle(GetVideosByCourseIdQuery request, CancellationToken cancellationToken)
        {
            try {

                

                // 1) اجلب الـ IQueryable من الريبو
                var query = _unitOfWork.Videos.GetAllVideosForCouse(request.CourseId);
                var course = query.FirstOrDefault().Course;
                string courseName = request.Culture == "ar" ? course.TitleAR : course.TitleEN;

                _unitOfWork.Videos.GetAllVideosForCouse(request.CourseId);
                // هنا تستخدم AutoMapper بشكل عادي بعد ما تجيب البيانات من قاعدة البيانات
                var result = _mapper.Map<List<VideoDto>>(query, opt =>
                {
                    opt.Items["culture"] = request.Culture;
                });



                result=result.Select(x =>
                {
                    x.CourseName = courseName;
                    x.CourseId = request.CourseId;
                    return x;
                }).ToList();
                // 4) رجع Response موحد
                return Response<IEnumerable<VideoDto>>
                    .SuccessResponse(result, "Videos fetched successfully");
            }
            catch(Exception ex)
            {
                return Response<IEnumerable<VideoDto>>.ErrorWithException(
                   "Error while fetching videos",
                   ex.Message,
                   data: null,
                   status: HttpStatusCode.InternalServerError
               );

            }
           
            
        }
    }
}
