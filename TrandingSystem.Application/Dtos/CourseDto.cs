using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrandingSystem.Domain.Entities;

namespace TrandingSystem.Application.Dtos
{
    public class CourseDto
    {
        public int CourseId { get; set; }

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

        public virtual Category Category { get; set; }

        public virtual ICollection<Community> Communities { get; set; } = new List<Community>();

        public virtual ICollection<CourseEnrollment> CourseEnrollments { get; set; } = new List<CourseEnrollment>();

        public virtual ICollection<CourseRating> CourseRatings { get; set; } = new List<CourseRating>();

        public virtual User CreateByNavigation { get; set; }

        public virtual ICollection<LiveSession> LiveSessions { get; set; } = new List<LiveSession>();

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

        public virtual ICollection<Video> Videos { get; set; } = new List<Video>();
    }
}
