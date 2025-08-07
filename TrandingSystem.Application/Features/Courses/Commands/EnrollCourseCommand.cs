using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.Application.Common.Response;

namespace TrandingSystem.Application.Features.Courses.Commands
{
    public class EnrollCourseCommand : IRequest<Response<bool>>
    {
        public int CourseId { get; set; }
        public int UserId { get; set; }
        public EnrollCourseCommand(int courseId, int userId)
        {
            CourseId = courseId;
            UserId = userId;
        }
    }
}
