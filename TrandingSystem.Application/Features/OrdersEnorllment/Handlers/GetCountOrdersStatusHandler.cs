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

                var countOrdersPending = 0;
                var countOrdersAccepted = 0;
                var countOrdersCanceled = 0;
                if (request.Type == 1)
                {
                    countOrdersPending = AllOrders.Count(x => x.OrderStatus == 2 && x.VideoId  != null);
                    countOrdersAccepted = AllOrders.Count(x => x.OrderStatus == 1 && x.VideoId != null);
                    countOrdersCanceled = AllOrders.Count(x => x.OrderStatus == 0 && x.VideoId != null);
                }   
                else
                {
                    countOrdersPending = AllOrders.Count(x => x.OrderStatus == 2 && x.VideoId == null);
                    countOrdersAccepted = AllOrders.Count(x => x.OrderStatus == 1 && x.VideoId== null);
                    countOrdersCanceled = AllOrders.Count(x => x.OrderStatus == 0 && x.VideoId == null);
                }
                  
                CountOrdersEnorllmentStatus CountOrders = new CountOrdersEnorllmentStatus()
                {
                    CountOrdersPending =  countOrdersPending ,
                    CountOrdersAccepted = countOrdersAccepted,
                    CountOrdersCanceled = countOrdersCanceled,
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
