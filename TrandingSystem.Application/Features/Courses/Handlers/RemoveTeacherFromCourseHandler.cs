using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Features.Courses.Commands;
using TrandingSystem.Domain.Interfaces;

namespace TrandingSystem.Application.Features.Courses.Handlers
{
    public class RemoveTeacherFromCourseHandler : IRequestHandler<RemoveTeacherFromCourseCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RemoveTeacherFromCourseHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public Task<Response<bool>> Handle(RemoveTeacherFromCourseCommand request, CancellationToken cancellationToken)
        {
            var resutl = _unitOfWork.Courses.RemoveTeacherFromCourse(request.CourseId, request.TeacherId);
            if (resutl == false)
            {
                return Task.FromResult(Response<bool>.ErrorResponse("No teachers found", resutl, System.Net.HttpStatusCode.NotFound));
            }
            return Task.FromResult(Response<bool>.SuccessResponse(resutl));
        }
    }
}
