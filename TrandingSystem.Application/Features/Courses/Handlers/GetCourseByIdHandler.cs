using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Dtos;
using TrandingSystem.Application.Features.Courses.Queries;
using TrandingSystem.Domain.Interfaces;

namespace TrandingSystem.Application.Features.Courses.Handlers
{
    internal class GetCourseByIdHandler : IRequestHandler<GetCourseByIdQuery, Response<CourseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCourseByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<Response<CourseDto>> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var course = _unitOfWork.Courses.ReadById(request.CourseId);
                if (course != null)
                {
                    var dto = _mapper.Map<CourseDto>(course);
                    dto.Lectures = _mapper.Map<List<UserDto>>(_unitOfWork.Courses.GetLecturesByCourseId(dto.CourseId));
                    dto.EnrolledUsers = _mapper.Map<List<UserDto>>(_unitOfWork.Courses.GetEnrolledUsers(dto.CourseId));
                    return Task.FromResult(Response<CourseDto>.SuccessResponse(dto));
                }
                return Task.FromResult(Response<CourseDto>.ErrorResponse("there is no course with this criteria",null,HttpStatusCode.NotFound));
            }
            catch(Exception ex)
            {
                return Task.FromResult(Response<CourseDto>.ErrorWithException("an error occurred", ex.Message));

            }

        }
    }
}
