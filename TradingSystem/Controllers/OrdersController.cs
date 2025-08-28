using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Features.OrdersEnorllment.Commands;
using TrandingSystem.Application.Features.OrdersEnorllment.Queries;
using TrandingSystem.Domain.Entities;
using TrandingSystem.Models;

namespace TrandingSystem.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IMediator _mediator;
        private readonly UserManager<User> _userManager;

        public OrdersController(IMediator mediator,UserManager<User> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }
        public IActionResult AllOrders()
        {
            // Get All Count Status Orders
            var response = _mediator.Send(new GetCountOrdersStatusQuery(0)).Result;

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
        public async Task<IActionResult> GetOrderByOrderStatus(int OrderStatus,int Type=0)
        {
            // Get All Order By OrderStatus
            var Response = await _mediator.Send(new GetOrdersByOrderStatusQuery(OrderStatus, Type));
            if (Response.Status== System.Net.HttpStatusCode.InternalServerError) {
                return RedirectToAction("Error", "Home", new
                {
                    status = (int)Response.Status,
                    message = Response.Message
                });

            }

      
            return PartialView("_PartialOdersEnrollemnt", Response.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderVideoByOrderStatus(int OrderStatus, int Type = 0)
        {
            // Get All Order By OrderStatus
            var Response = await _mediator.Send(new GetOrdersByOrderStatusQuery(OrderStatus, Type));
            if (Response.Status == System.Net.HttpStatusCode.InternalServerError)
            {
                return RedirectToAction("Error", "Home", new
                {
                    status = (int)Response.Status,
                    message = Response.Message
                });

            }


            return PartialView("_PartialOdersVideoEnrollemnt", Response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeOrderStatus(int orderId,int newStatus,int type)
        {
            // This gets the user object from the Identity system
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return Unauthorized(new { message = "Not logged in" });
            var res = _mediator.Send(new ConfirmedOrderRequestCommand(orderId, DateTime.Now, user.Id, newStatus, type)).Result;
            return Json(res);
        }


        /// For Vides Orders
        /// 
        public IActionResult AllOrdersVideo()
        {
            // Get All Count Status Orders
            var response = _mediator.Send(new GetCountOrdersStatusQuery(1)).Result;

            if (!response.Success)
            {
                return RedirectToAction("Error", "Home", new
                {
                    status = (int)response.Status,
                    message = response.Message
                });
            }
            return View(response.Data);
        }
    }
}
