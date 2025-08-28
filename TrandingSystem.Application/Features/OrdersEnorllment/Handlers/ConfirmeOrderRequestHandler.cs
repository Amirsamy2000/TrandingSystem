
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

        public ConfirmeOrderRequestHandler(IUnitOfWork unitOfWork, IStringLocalizer<ValidationMessages> localizer)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
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
               
                if (request.Status==1)
                {
                   
                    order.EnrollmentDate = request.CreatedAt;
                    var communities = _unitOfWork.Courses.ReadById(order.CourseId??0).Communities;
                    if (communities is not null)
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

                        }

                    }
                }
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
