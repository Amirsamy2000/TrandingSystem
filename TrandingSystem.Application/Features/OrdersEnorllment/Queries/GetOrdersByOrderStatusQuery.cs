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

        public int Type { set; get; } // 0 For Courses and is defualt, 1 for order video
        public GetOrdersByOrderStatusQuery(int orderStatus, int type)
        {
            OrderStatus = orderStatus;
            Type = type;
        }
    }
}
