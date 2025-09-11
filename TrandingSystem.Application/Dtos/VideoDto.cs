
using TrandingSystem.Domain.Entities;

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

        public bool HassAccess { get; set; }
        public int StatusOrder { set; get; } // 0 pending ,1 accpet


    }
}
