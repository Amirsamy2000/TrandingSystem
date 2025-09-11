

using MediatR;
using Microsoft.Extensions.Localization;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Features.LiveSessions.Commands;
using TrandingSystem.Application.Resources;
using TrandingSystem.Domain.Entities;
using TrandingSystem.Domain.Interfaces;
using TrandingSystem.Infrastructure.Constants;
 
namespace TrandingSystem.Application.Features.LiveSessions.Handles
{
    public class EnrollmentInPaidSessionHandler : IRequestHandler<EnrollmentInPaidSessionCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<ValidationMessages> _localizer;
        private readonly IFileService _fileService;


        public EnrollmentInPaidSessionHandler(IUnitOfWork unitOfWork, IStringLocalizer<ValidationMessages> localizer, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
            _fileService = fileService;
         }
        public async Task<Response<bool>> Handle(EnrollmentInPaidSessionCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var live = _unitOfWork.LiveSessionRepositry.ReadById(request.SessionId);
                if (live == null)
                {
                    return Response<bool>.ErrorResponse(_localizer["notFoundMessage"]);
                }
              
                if (live.CourseEnrollments.Any(x => x.UserId == request.UserId && (x.OrderStatus == 1 || x.OrderStatus == 2)))
                {
                    return Response<bool>.ErrorResponse(_localizer["EnrollmentalreadyLive"]);

                }
                string imageUrl = await _fileService.SaveImageAsync(request.RecieptImage, ConstantPath.PathVideoImage);

                if (string.IsNullOrEmpty(imageUrl))
                {
                    return Response<bool>.ErrorResponse(_localizer["AddImageFail"]);
                }
                var order = new Video_CourseEnrollment()
                {
                    liveId = request.SessionId,
                    CourseId = live.CourseId,
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
