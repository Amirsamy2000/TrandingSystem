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
using TrandingSystem.Domain.Entities;
using TrandingSystem.Domain.Interfaces;
using TrandingSystem.Infrastructure.Constants;

namespace TrandingSystem.Application.Features.Courses.Handlers
{
    internal class UpdateCourseHandler : IRequestHandler<UpdateCourseCommand, Response<CourseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;
        private readonly IFileService _imageService;


        public UpdateCourseHandler(IUnitOfWork unitOfWork, IMapper mapper, IFileService imageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _imageService = imageService;
        }
        public Task<Response<CourseDto>> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existingCourse = _unitOfWork.Courses.ReadById(request.CourseId);
                if (existingCourse == null)
                {
                    return Task.FromResult(Response<CourseDto>.ErrorResponse("Invalid course data"));
                }

                // update the existing entity
                _mapper.Map(request, existingCourse);

                if (request.CourseImage != null)
                {
                    existingCourse.ImageCourseUrl = _imageService.SaveImageAsync(request.CourseImage, ConstantPath.PathdCoursesImages).Result;
                }

                var result = _unitOfWork.Courses.Update(existingCourse);


                if (result == null)
                {
                    return Task.FromResult(Response<CourseDto>.ErrorResponse("Failed to create course"));
                }
                return Task.FromResult(Response<CourseDto>.SuccessResponse(_mapper.Map<CourseDto>(result), "Course created successfully"));
            }
            catch (Exception ex)
            {
                return Task.FromResult(Response<CourseDto>.ErrorResponse($"An error occurred while processing the request: {ex.Message}"));
            }

        }
    }
}
