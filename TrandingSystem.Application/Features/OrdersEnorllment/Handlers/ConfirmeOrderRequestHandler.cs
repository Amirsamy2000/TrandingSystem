
using MediatR;
using Microsoft.Extensions.Localization;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Features.OrdersEnorllment.Commands;
using TrandingSystem.Application.Resources;
using TrandingSystem.Domain.Entities;
using TrandingSystem.Domain.Interfaces;

namespace TrandingSystem.Application.Features.OrdersEnorllment.Handlers
{
    public class ConfirmeOrderRequestHandler : IRequestHandler<ConfirmedOrderRequestCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<ValidationMessages> _localizer;
        private readonly IFileService _fileService;
        private readonly INotificationService _notificationService;



        public ConfirmeOrderRequestHandler(IUnitOfWork unitOfWork, IStringLocalizer<ValidationMessages> localizer, IFileService fileService, INotificationService notificationService)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
            _fileService = fileService;
            _notificationService = notificationService;
        }

        public async Task<Response<bool>> Handle(ConfirmedOrderRequestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var order = _unitOfWork.ordersEnorllment.ReadById(request.OrderId);
                if (order == null)
                {
                    return Response<bool>.ErrorResponse(
                        _localizer["OrderNotFound"]);
                }

                order.ConfirmedBy = request.ConfirmedBy;
                order.OrderStatus = (byte)request.Status;
                order.IsConfirmed = true;
                var EmailTemp = new Domain.Helper.EmailBody()
                {
                    dir = _localizer["dir"],
                    Subject = _localizer["stieName"],
                    StieName = _localizer["stieName"],
                    Hi = _localizer["hi"],
                    info1 = _localizer["infoorder1"],
                    info2 = _localizer["infoVido3"] + " " + order.Course.TitleEN,
                    info3 = "",
                    contact = _localizer["contact"],
                    namebtn="Show Course",
                    UserName = order.User.FullName,
                    ActionUrl = $"http://saifalqadi.runasp.net/Home/Courses"

                };
                if (request.Status==1)
                {
                    EmailTemp.info1 = _localizer["infoorder2"];
                    order.EnrollmentDate = request.CreatedAt;
                   // var communities = _unitOfWork.Courses.ReadById((int)order.OrderStatus).Communities;
                    var communities = _unitOfWork.Courses.ReadById(order.CourseId??0).Communities;

                    if (communities is not null && request.Type==0 )
                    {
                        foreach (var comm in communities)
                        {
                            var member = new CommunityMember()
                            {
                                CommunityId=comm.CommunityId,
                                UserId = order.UserId,
                                IsBlocked = false,
                                JoinedAt = DateTime.UtcNow,
                            };
                            _unitOfWork.CommunityMember.Create(member);
                            await _unitOfWork.SaveChangesAsync();

                        }

                    }
                    
                }
                else
                {

                    await _fileService.DeleteImageAsync(order.ReceiptImagePath);
                }
                _notificationService.SendMailForUserAfterCreateBodey(order.User.Email, _localizer["FormalSub"], EmailTemp);

                _unitOfWork.ordersEnorllment.Update(order);
                await _unitOfWork.SaveChangesAsync();
                return Response<bool>.SuccessResponse(true, _localizer["GeneralOperationDone"]);

            }
            catch (Exception ex)
            {
                return Response<bool>.ErrorResponse(
                    _localizer["GeneralError"]);





            }
        }
    }
}
