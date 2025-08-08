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
using TrandingSystem.Infrastructure.Constants;

namespace TrandingSystem.Application.Features.Courses.Handlers
{
    public class EnrollCourseHandler : IRequestHandler<EnrollCourseCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IFileService _imageService;


        public EnrollCourseHandler(IUnitOfWork unitOfWork, IMapper mapper, IFileService imageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _imageService = imageService;
        }
        public Task<Response<bool>> Handle(EnrollCourseCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            try
            {
                var isEnrolled = _unitOfWork.Courses.IsCourseEnrolled(request.CourseId, request.UserId);
                if (isEnrolled)
                {
                    return Task.FromResult(Response<bool>.ErrorResponse("User is already enrolled in this course"));
                }


                string imageUrl = _imageService.SaveImageAsync(request.ReceiptImage, ConstantPath.PathdReceiptsImage).Result;
                
                
                
                
                var result = _unitOfWork.Courses.EnrollCourse(request.CourseId, request.UserId,imageUrl);


                if (result)
                {
                    return Task.FromResult(Response<bool>.SuccessResponse(true, "User enrolled successfully"));
                }
                return Task.FromResult(Response<bool>.ErrorResponse("Failed to enroll user in the course"));
            }
            catch (Exception ex)
            {
                return Task.FromResult(Response<bool>.ErrorWithException("An error occurred while processing the request", ex.Message));
            }
        }
    }
}
