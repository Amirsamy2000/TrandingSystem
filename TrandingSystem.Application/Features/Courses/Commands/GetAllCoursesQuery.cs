using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrandingSystem.Domain.Entities;

namespace TrandingSystem.Application.Features.Courses.Commands
{
    public class GetAllCoursesQuery : IRequest<List<Course>>
    {
    }
}
