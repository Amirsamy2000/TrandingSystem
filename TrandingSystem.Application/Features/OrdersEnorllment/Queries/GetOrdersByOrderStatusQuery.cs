 using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Dtos;

namespace TrandingSystem.Application.Features.OrdersEnorllment.Queries
{
    public class GetOrdersByOrderStatusQuery:IRequest<Response<IEnumerable<OrdersDto>>>
    {
        public int OrderStatus { get; set; } // 0: Canceled, 1: Accepted, 2: Pending
        public GetOrdersByOrderStatusQuery(int orderStatus)
        {
            OrderStatus = orderStatus;
        }
    }
}
