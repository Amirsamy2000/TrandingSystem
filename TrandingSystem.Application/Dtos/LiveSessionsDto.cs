using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrandingSystem.Domain.Entities;

namespace TrandingSystem.Application.Dtos
{
    public class LiveSessionsDto
    {
        public int SessionId { get; set; }

        public string Title { get; set; }


        public int CourseId { get; set; }

 
        public string Description { get; set; }

        public string YoutubeLink { get; set; }

        public DateTime ScheduledAt { get; set; }
        public string ImageSessionUrl { get; set; }

        public bool? IsLocked { get; set; }
        public TimeSpan? ScheduledTime { get; set; }

        public decimal? Cost { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int CreadteBy { get; set; }

        public bool? IsActive { get; set; }
        public string CourseName { get; set; }

        public bool HassAccess { get; set; }
        public int StatusOrder { set; get; } // 0 pending ,1 accpet
    }
}
