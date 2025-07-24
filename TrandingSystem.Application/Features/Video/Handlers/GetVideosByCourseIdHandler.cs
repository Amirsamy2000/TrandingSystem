using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Dtos;
using TrandingSystem.Application.Features.Video.Queries;
using TrandingSystem.Domain.Entities;
using TrandingSystem.Domain.Interfaces;
using TrandingSystem.Domain.Entities;

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

                _unitOfWork.Videos.GetAllVideosForCouse(request.CourseId);
                // هنا تستخدم AutoMapper بشكل عادي بعد ما تجيب البيانات من قاعدة البيانات
                var result = _mapper.Map<List<VideoDto>>(query, opt =>
                {
                    opt.Items["culture"] = request.Culture;
                });


                

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
