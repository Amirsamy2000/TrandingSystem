using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Dtos;

namespace TrandingSystem.Application.Features.Courses.Commands
{
    public class AssignTeacherToCourseCommand:IRequest<Response<List<UserDto>>>
    {
        public int CourseId { get; set; }
        public List<int> TeachersId { get; set; }
    }
}
