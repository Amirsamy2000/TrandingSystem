
using MediatR;
using Microsoft.Extensions.Localization;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Features.OrdersEnorllment.Commands;
using TrandingSystem.Application.Resources;
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
                order.EnrollmentDate = request.CreatedAt;
                order.OrderStatus = request.Status ? (byte)1 : (byte)0;
                order.IsConfirmed = true;
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
