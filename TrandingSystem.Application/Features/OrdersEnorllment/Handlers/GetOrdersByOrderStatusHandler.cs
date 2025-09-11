using MediatR;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Dtos;
using TrandingSystem.Application.Features.OrdersEnorllment.Queries;
using TrandingSystem.Application.Resources;
using TrandingSystem.Domain.Interfaces;

namespace TrandingSystem.Application.Features.OrdersEnorllment.Handlers
{
    public class GetOrdersByOrderStatusHandler : IRequestHandler<GetOrdersByOrderStatusQuery, Response<IEnumerable<OrdersDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<ValidationMessages> _localizer;
        public GetOrdersByOrderStatusHandler(IUnitOfWork unitOfWork, IStringLocalizer<ValidationMessages> localizer)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Response<IEnumerable<OrdersDto>>> Handle(GetOrdersByOrderStatusQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var Orders = _unitOfWork.ordersEnorllment.Read();
               
                if (request.Type==1){
                    Orders= Orders.Where(x => x.OrderStatus == request.OrderStatus & x.CourseId!= null & x.VideoId!=null).ToList();
                }
                else if(request.Type == 2)
                {
                    Orders = Orders.Where(x => x.OrderStatus == request.OrderStatus & x.CourseId != null & x.liveId != null).ToList();

                }
                else
                {
                    Orders = Orders.Where(x => x.OrderStatus == request.OrderStatus & x.VideoId == null).ToList();
                }
               
                
                if (Orders.Count()<0) return Response< IEnumerable <OrdersDto>>.ErrorResponse( _localizer["NoDataFound"], (IEnumerable<OrdersDto>) Orders );

                IEnumerable<OrdersDto> ordersDtos = Orders.Select(order => new OrdersDto
                {
                    EnrollmentId = order.EnrollmentId,
                    EnrollmentDate = order?.EnrollmentDate,
                    IsConfirmed = order.IsConfirmed,
                    ReceiptImagePath = order.ReceiptImagePath,
                    OrderStatus = order.OrderStatus,
                    ConfirmedBy = order.ConfirmedBy,
                    CreatedAt = order.CreatedAt,
                    UserName = order.User.FullName,
                    UserEmail = order.User.Email,
                    UserMobile = order.User.Mobile,
                    CourseName = order.Course.TitleEN,
                    CostCourse = order.Course.Cost,
                    IsPaid = order.Course.IsFullyFree,
                    CashPhoneNum = order.CashPhoneNum,
                    VideoName = order.Video?.TitleEN ?? "Un",
                    VideoCost = order.Video?.Cost ?? 0,
                    LiveCost = order.Live?.Cost ?? 0,
                    LiveName = order.Live?.TitleEN ?? "Un",

                });


                return Response<IEnumerable<OrdersDto>>.SuccessResponse(ordersDtos);



            }
            catch
            {

                return Response<IEnumerable<OrdersDto>>.ErrorResponse(_localizer["errorGetOrders"],status:System.Net.HttpStatusCode.InternalServerError);
            }


           
        }
    }
}
