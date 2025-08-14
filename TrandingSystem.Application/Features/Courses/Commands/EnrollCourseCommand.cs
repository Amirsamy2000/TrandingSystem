using MediatR;
using Microsoft.AspNetCore.Http;
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
        public IFormFile? ReceiptImage { get; set; }
        public EnrollCourseCommand(int courseId, int userId, IFormFile Reciept)
        {
            CourseId = courseId;
            UserId = userId;
            ReceiptImage = Reciept;
        }
    }
}
