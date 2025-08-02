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
using TrandingSystem.Infrastructure.Repositories;

namespace TrandingSystem.Application.Features.Courses.Handlers
{
    public class DeleteCourseHandler : IRequestHandler<DeleteCourseCommand, Response<CourseDto>>
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IMapper _mapper;

        public DeleteCourseHandler(IMapper mapper,IUnitOfWork UnitOfWork, ICourseRepository courseRepository)
        {
            _UnitOfWork = UnitOfWork;
            _mapper = mapper;
        }
        public Task<Response<CourseDto>> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            try { 
            
                var result = _UnitOfWork.Courses.Delete(request.courseId);
                var dto = _mapper.Map<CourseDto>(result);
                

                return Task.FromResult(Response<CourseDto>.SuccessResponse(dto));
            }
            catch (Exception ex)
            {
                return Task.FromResult(Response<CourseDto>.ErrorWithException($"An error occurred while deleting the course",ex.Message));
            }

            
        }
    }
}
