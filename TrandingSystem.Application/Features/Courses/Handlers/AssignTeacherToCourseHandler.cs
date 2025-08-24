using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Dtos;
using TrandingSystem.Application.Features.Courses.Commands;
using TrandingSystem.Domain.Interfaces;

namespace TrandingSystem.Application.Features.Courses.Handlers
{
    public class AssignTeacherToCourseHandler : IRequestHandler<AssignTeacherToCourseCommand, Response<List<UserDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AssignTeacherToCourseHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<Response<List<UserDto>>> Handle(AssignTeacherToCourseCommand request, CancellationToken cancellationToken)
        {
            var resutl = _unitOfWork.Courses.AssignTeacherToCourse(request.CourseId, request.TeachersId);
            if (resutl == null || !resutl.Any())
            {
                return Task.FromResult(Response<List<UserDto>>.ErrorResponse("No teachers found", null, System.Net.HttpStatusCode.NotFound));
            }
            List<UserDto> usersDto = _mapper.Map<List<UserDto>>(resutl);
            return Task.FromResult(Response<List<UserDto>>.SuccessResponse(usersDto));
        }
    }
}
