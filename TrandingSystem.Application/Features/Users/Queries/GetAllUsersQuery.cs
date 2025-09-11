using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Dtos;

namespace TrandingSystem.Application.Features.Users.Queries
{
    public class GetAllUsersQuery : IRequest<Response<List<UserDto>>>
    {
    }
}
