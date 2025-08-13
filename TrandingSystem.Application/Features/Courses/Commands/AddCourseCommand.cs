using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Domain.Entities;

namespace TrandingSystem.Application.Features.Courses.Commands
{
    public class AddCourseCommand : IRequest<Response<Course>>
    {
        public string TitleEN { get; set; }

        public string TitleAR { get; set; }

        public string DescriptionEN { get; set; }

        public string DescriptionAR { get; set; }

        public int CategoryId { get; set; }

        public decimal Cost { get; set; }

        public bool? CommunityAutoCreate { get; set; }

        public bool IsLive { get; set; }

        public int? CreateBy { get; set; }

        public DateTime? CreateAt { get; set; } 

        public bool? IsFullyFree { get; set; }

        public bool? IsActive { get; set; }
        public IFormFile? CourseImage { get; set; }



    }
}
