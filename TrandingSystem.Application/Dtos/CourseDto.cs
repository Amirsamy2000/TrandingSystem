using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrandingSystem.Domain.Entities;
using TrandingSystem.Domain.Interfaces;

namespace TrandingSystem.Application.Dtos
{
    public class CourseDto
    {
        
        public string TitleEN { get; set; }

        public string TitleAR { get; set; }

        public string DescriptionEN { get; set; }

        public string DescriptionAR { get; set; }

        public int CategoryId { get; set; }
        public string categoryName { get; set; }

        public decimal Cost { get; set; }

        public bool? CommunityAutoCreate { get; set; }

        public bool IsLive { get; set; }

        public int? CreateBy { get; set; }

        public DateTime? CreateAt { get; set; }

        public bool? IsFullyFree { get; set; }

        public bool? IsActive { get; set; }

        public string? ImageCourseUrl { get; set; }
    }
}
