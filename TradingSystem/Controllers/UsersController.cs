using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TrandingSystem.Application.Features.Users.Queries;

namespace TrandingSystem.Controllers
{
    public class UsersController : Controller
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var usersResponse = await _mediator.Send(new GetAllUsersQuery());
            
            return View(usersResponse);
        }
    }
}
