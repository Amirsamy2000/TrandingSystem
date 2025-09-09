using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TrandingSystem.Application.Features.Users.Commands;
using TrandingSystem.Application.Features.Users.Queries;
using TrandingSystem.Domain.Entities;

namespace TrandingSystem.Controllers
{
    public class UsersController : Controller
    {
        private readonly IMediator _mediator;
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        public UsersController(IMediator mediator, RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            _mediator = mediator;
            _roleManager = roleManager;
            _userManager = userManager;
        }


        public async Task<IActionResult> Index()
        {
            var usersResponse = await _mediator.Send(new GetAllUsersQuery());
            
            return View(usersResponse);
        }

        public async Task<IActionResult> SwitchColumn(int userId, int SwitchIsBlockedColumn,int SwitchEmailConfirmedColumn)
        {
            var UpdatedUserResponse = await _mediator.Send(new SwitchIsBlockedEmailConfirmedCommand(userId, SwitchIsBlockedColumn, SwitchEmailConfirmedColumn));

            return Ok(UpdatedUserResponse);
        }

        [HttpGet]
        public IActionResult GetAllRoles()
        {
            var roles = _roleManager.Roles.ToList();
            return Ok(roles);
        }

        // Change a user's role (replace old roles with a new one)
        public async Task<IdentityResult> ChangeUserRole(string userId, string newRole)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });

            // Ensure role exists
            if (!await _roleManager.RoleExistsAsync(newRole))
                return IdentityResult.Failed(new IdentityError { Description = $"Role '{newRole}' does not exist." });

            // Remove old roles
            var currentRoles = await _userManager.GetRolesAsync(user);
            var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!removeResult.Succeeded)
                return removeResult;

            // Add new role
            var addResult = await _userManager.AddToRoleAsync(user, newRole);
            return addResult;
        }

        // Action to get the first role of a user
        public async Task<IActionResult> GetFirstUserRole(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound("User not found.");

            var roles = await _userManager.GetRolesAsync(user);
            var firstRole = roles.FirstOrDefault();

            return Ok(firstRole ?? "No Role Assigned");
        }

    }
}
