using Amazon.Runtime.Internal;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Features.Video.Commands;
using TrandingSystem.Application.Resources;
using TrandingSystem.Domain.Entities;
using TrandingSystem.Domain.Interfaces;
using TrandingSystem.Infrastructure.Constants;

namespace TrandingSystem.Application.Features.Video.Handlers
{
    public class EnrollmentInPaidVideoHandler : IRequestHandler<EnrollmentInPaidVideoCommand, Response<bool>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<ValidationMessages> _localizer;
        private readonly IFileService _fileService;


        public EnrollmentInPaidVideoHandler(IUnitOfWork unitOfWork, IStringLocalizer<ValidationMessages> localizer, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
            _fileService = fileService;
        }
        public async Task<Response<bool>> Handle(EnrollmentInPaidVideoCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var video =  _unitOfWork.Videos.ReadById(request.VideoId);
                if (video == null)
                {
                    return Response<bool>.ErrorResponse(_localizer["notFoundMessage"]);
                }
               
                if (video.CourseEnrollments.Any(x => x.UserId == request.UserId&&x.VideoId==request.VideoId&&(x.OrderStatus==1||x.OrderStatus==2)))
                {
                    return Response<bool>.ErrorResponse(_localizer["Enrollmentalreadyvideo"]);

                }
                string imageUrl = await _fileService.SaveImageAsync(request.RecieptImage, ConstantPath.PathVideoImage);

                if (string.IsNullOrEmpty(imageUrl))
                {
                    return Response<bool>.ErrorResponse(_localizer["AddImageFail"]);
                }

                var order = new Video_CourseEnrollment()
                {
                    VideoId = request.VideoId,
                    CourseId = video.CourseId,
                    UserId = request.UserId,
                    ReceiptImagePath = imageUrl,
                    OrderStatus = 2,
                    CreatedAt = DateTime.Now,
                    

                };
                _unitOfWork.ordersEnorllment.Create(order);
                await _unitOfWork.SaveChangesAsync();

                return Response<bool>.SuccessResponse(true, _localizer["GeneralOperationDone"]);
            }
            catch
            {
                return Response<bool>.ErrorResponse(_localizer["GeneralError"]);

            }

        }


    }
}
