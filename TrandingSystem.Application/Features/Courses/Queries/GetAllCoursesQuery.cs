using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Dtos;
using TrandingSystem.Domain.Entities;

namespace TrandingSystem.Application.Features.Courses.Queries
{
    public class GetAllCoursesQuery : IRequest<Response<List<CourseDto>>>
    {
    }
}
