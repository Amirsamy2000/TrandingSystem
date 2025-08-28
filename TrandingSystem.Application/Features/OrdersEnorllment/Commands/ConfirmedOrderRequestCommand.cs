using Amazon.Runtime.Internal;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.Application.Common.Response;

namespace TrandingSystem.Application.Features.OrdersEnorllment.Commands
{
    public class ConfirmedOrderRequestCommand : IRequest<Response<bool>>
    {
        public int OrderId { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public int  ConfirmedBy{set;get;}
        public int Type { set; get; } = 0;


        public ConfirmedOrderRequestCommand(int orderid, DateTime createdAt, int confirmedBy, int status, int type)
        {
            OrderId = orderid;
            CreatedAt = createdAt;
            ConfirmedBy = confirmedBy;
            Status = status;
            Type = type;
        }

    }
}
