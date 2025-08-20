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
using TrandingSystem.Application.Features.Users.Queries;
using TrandingSystem.Domain.Entities;
using TrandingSystem.Domain.Interfaces;

namespace TrandingSystem.Application.Features.Users.Handlers
{
    public class ReadNotAssignedTeachersHandler : IRequestHandler<ReadNotAssignedTeachersQuery, Response<List<UserDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;


        public ReadNotAssignedTeachersHandler(IUnitOfWork unitOfWork, IUserRepository userRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<Response<List<UserDto>>> Handle(ReadNotAssignedTeachersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var users = _unitOfWork.Users.ReadAllTeacher();

                List<UserDto> lectureresDto = new List<UserDto>(); ;
                if (request.CourseId > 0)
                {
                    lectureresDto = _mapper.Map<List<UserDto>>(_unitOfWork.Courses.GetLecturesByCourseId(request.CourseId).ToList());
                    var lectureresIds = lectureresDto.Select(l=>l.Id);
                    // Filter out users who are already assigned to the specified course
                    users = users.Where(u => !lectureresIds.Contains(u.Id)).ToList();
                }


                List<UserDto> usersDto = _mapper.Map<List<UserDto>>(users);

                if (usersDto == null)
                {
                    return Task.FromResult(Response<List<UserDto>>.ErrorResponse("No teachers found", null, HttpStatusCode.NotFound));
                }

                return Task.FromResult(Response<List<UserDto>>.SuccessResponse(usersDto, "Everything is okay", lectureresDto));
            }
            catch (Exception ex)
            {
                return Task.FromResult(Response<List<UserDto>>.ErrorWithException("An error occurred", ex.Message));
            }
        }
    }
}
