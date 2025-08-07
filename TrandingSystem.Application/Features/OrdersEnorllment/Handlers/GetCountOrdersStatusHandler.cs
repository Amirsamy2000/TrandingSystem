using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Dtos;
using TrandingSystem.Application.Features.OrdersEnorllment.Queries;
using TrandingSystem.Application.Resources;
using TrandingSystem.Domain.Interfaces;

namespace TrandingSystem.Application.Features.OrdersEnorllment.Handlers
{
    public class GetCountOrdersStatusHandler : IRequestHandler<GetCountOrdersStatusQuery, Response<CountOrdersEnorllmentStatus>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<ValidationMessages> _localizer;
        public GetCountOrdersStatusHandler(IUnitOfWork unitOfWork,IStringLocalizer<ValidationMessages> localizer)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }
        public async Task<Response<CountOrdersEnorllmentStatus>> Handle(GetCountOrdersStatusQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var AllOrders = _unitOfWork.ordersEnorllment.Read();
                
                CountOrdersEnorllmentStatus CountOrders = new CountOrdersEnorllmentStatus()
                {
                    CountOrdersPending = AllOrders.Count(x => x.OrderStatus == 2),
                    CountOrdersAccepted = AllOrders.Count(x => x.OrderStatus == 1),
                    CountOrdersCanceled = AllOrders.Count(x => x.OrderStatus == 0),
                };

                return Response<CountOrdersEnorllmentStatus>.SuccessResponse(CountOrders, "");
            }
            catch (Exception ex)
            {
                return Response<CountOrdersEnorllmentStatus>.ErrorResponse(_localizer["errorGetOrders"]);

            }
        }
    }
}
