using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrandingSystem.Domain.Entities;
using TrandingSystem.Domain.Interfaces;
using AutoMapper;
using TrandingSystem.Application.Features.Courses.Commands;
using TradingSystem.Application.Common.Response;

namespace TrandingSystem.Application.Features.Courses.Handlers
{
    internal class AddCourseHandler : IRequestHandler<AddCourseCommand, Response<Course>>
    {
        private readonly ICourseRepository _CourseRepository;
        private readonly IMapper _mapper;

        public AddCourseHandler(ICourseRepository CourseRepository, IMapper mapper)
        {
            _CourseRepository = CourseRepository;
            _mapper = mapper;
        }

        public async Task<Response<Course>> Handle(AddCourseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var course = _mapper.Map<Course>(request);
                if (course == null)
                {
                    return Response<Course>.ErrorResponse("Invalid course data");
                }

                var result = _CourseRepository.Create(course);

                if (result == null)
                {
                    return Response<Course>.ErrorResponse("Failed to create course");
                }
                return Response<Course>.SuccessResponse(result, "Course created successfully");
            }
            catch (Exception ex)
            {
                return Response<Course>.ErrorResponse($"An error occurred while processing the request: {ex.Message}");
            }
            
        }
    }
}
