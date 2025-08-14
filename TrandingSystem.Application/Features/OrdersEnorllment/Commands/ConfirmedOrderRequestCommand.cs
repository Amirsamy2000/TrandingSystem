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
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public int  ConfirmedBy{set;get;}


        public ConfirmedOrderRequestCommand(int orderid, DateTime createdAt, int confirmedBy, bool status)
        {
            OrderId = orderid;
            CreatedAt = createdAt;
            ConfirmedBy = confirmedBy;
            Status = status;
        }

    }
}
