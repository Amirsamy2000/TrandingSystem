using Amazon.Runtime.Internal;
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
    public class GetCountOrdersStatusQuery:IRequest<Response<CountOrdersEnorllmentStatus>>
    {
        public int Type { get; set; } = 0; // 0 for course , 1 for video
      public  GetCountOrdersStatusQuery(int type) {
            Type = type;
        }


    }
}
