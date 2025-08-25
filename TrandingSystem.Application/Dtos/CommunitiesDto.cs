using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrandingSystem.Application.Dtos
{
    public class CommunitiesDto
    {
        public int CommunityId { get; set; }

        public string Title { get; set; }

        public bool? IsDefault { get; set; }

        public int? CourseId { get; set; }

        public bool? IsClosed { get; set; }

        public bool? IsAdminOnly { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string ?CourseTitle { get; set; }
        public int CountSubucribtor { set; get; }

        public string? LastMessage { set; get; }
        public string? Sender { set; get; }
        public DateTime? LastMessageTime { set; get; }

    }
}
