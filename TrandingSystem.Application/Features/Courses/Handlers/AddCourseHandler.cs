using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Features.Courses.Commands;
using TrandingSystem.Domain.Entities;
using TrandingSystem.Domain.Interfaces;
using TrandingSystem.Infrastructure.Constants;

namespace TrandingSystem.Application.Features.Courses.Handlers
{
    internal class AddCourseHandler : IRequestHandler<AddCourseCommand, Response<Course>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;
        private readonly IFileService _imageService;


        public AddCourseHandler(IUnitOfWork unitOfWork, IMapper mapper, IFileService imageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _imageService = imageService;
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

                if(request.CourseImage != null)
                {
                    course.ImageCourseUrl = _imageService.SaveImageAsync(request.CourseImage, ConstantPath.PathdCoursesImages).Result;

                }

                // TODO : Add created by field

                var result = _unitOfWork.Courses.Create(course);

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
