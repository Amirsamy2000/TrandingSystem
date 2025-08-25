using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrandingSystem.Application.Dtos;

namespace TrandingSystem.Application.Features.Roles.Queries
{
    public class GetUserRolesQuery : IRequest<List<RoleDto>>
    {
        public int UserId { get; set; }

        public GetUserRolesQuery(int userId)
        {
            UserId = userId;
        }
    }
}
