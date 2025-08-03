using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrandingSystem.Application.Dtos
{
    public class VideoDto
    {
        public int VideoId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageVideoUrl { get; set; }
        public bool IsPaid { get; set; }
        public decimal? Cost { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool? IsActive { get; set; }
        public string CourseName { get; set; }
        public int CourseId { get; set; }
    }
}
