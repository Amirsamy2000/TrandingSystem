using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TrandingSystem.Application.Dtos;
using TrandingSystem.Application.Features.Users.Queries;
using TrandingSystem.Domain.Entities;
using TrandingSystem.Domain.Interfaces;

namespace TrandingSystem.Application.Features.Users.Handlers
{
    public class ReadTeacherHandler : IRequestHandler<ReadTeachersQuery, List<UserDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;


        public ReadTeacherHandler(IUnitOfWork unitOfWork, IUserRepository userRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<List<UserDto>> Handle(ReadTeachersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var users = _unitOfWork.Users.ReadAllTeacher();

                if (users == null || !users.Any())
                {
                    return Response<List<CourseDto>>.ErrorResponse("No courses found", null, HttpStatusCode.NotFound);
                }

                var courseDtos = _mapper.Map<List<CourseDto>>(courses);


                foreach (var courseDto in courseDtos)
                {
                    courseDto.Lectures = _mapper.Map<List<UserDto>>(_unitOfWork.Courses.GetLecturesByCourseId(courseDto.CourseId));
                }
                return Response<List<CourseDto>>.SuccessResponse(courseDtos);
            }
            catch (Exception ex)
            {
                return Response<List<CourseDto>>.ErrorWithException("An error occurred", ex.Message);
            }
        }
    }
}
