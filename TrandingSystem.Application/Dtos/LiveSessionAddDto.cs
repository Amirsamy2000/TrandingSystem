using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrandingSystem.Domain.Entities;

namespace TrandingSystem.Application.Dtos
{
    public class LiveSessionAddDto
    {
        public int SessionId { get; set; }

        public string TitleEN { get; set; }

        public string TitleAR { get; set; }

        public int CourseId { get; set; }

        public string DescriptionEN { get; set; }

        public string DescriptionAR { get; set; }

        public string YoutubeLink { get; set; }
        public DateTime ScheduledAt { get; set; }
        public string ImageSessionUrl { get; set; }

        public bool? IsLocked { get; set; }

        public decimal? Cost { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int CreadteBy { get; set; }

        public bool? IsActive { get; set; }


    }
}
