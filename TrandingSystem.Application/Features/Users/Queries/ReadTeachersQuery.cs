using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrandingSystem.Application.Dtos;
using TrandingSystem.Domain.Entities;

namespace TrandingSystem.Application.Features.Users.Queries
{
    public class ReadTeachersQuery:IRequest<List<UserDto>>
    {

    }
}
