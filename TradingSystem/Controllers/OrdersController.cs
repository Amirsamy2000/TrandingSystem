using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TrandingSystem.Application.Features.OrdersEnorllment.Queries;
using TrandingSystem.Models;

namespace TrandingSystem.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IMediator _mediator;
        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public IActionResult AllOrders()
        {
            // Get All Count Status Orders
            var response = _mediator.Send(new GetCountOrdersStatusQuery()).Result;

            if (!response.Success) {
                return RedirectToAction("Error", "Home", new
                {
                    status = (int)response.Status,
                    message = response.Message
                });
            }
            return View(response.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderByOrderStatus(int OrderStatus)
        {
            // Get All Order By OrderStatus
            var Response = await _mediator.Send(new GetOrdersByOrderStatusQuery(OrderStatus));
            if (Response.Status== System.Net.HttpStatusCode.InternalServerError) {
                return RedirectToAction("Error", "Home", new
                {
                    status = (int)Response.Status,
                    message = Response.Message
                });

            }

            return PartialView("_PartialOdersEnrollemnt", Response.Data);
        }
    }
}
