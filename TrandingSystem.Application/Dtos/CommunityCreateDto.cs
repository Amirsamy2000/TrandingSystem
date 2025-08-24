using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrandingSystem.Application.Dtos
{
    public class CommunityCreateDto
    {
        public string Title { get; set; }
        public bool IsDefault { get; set; }
        public int? CourseId { get; set; }
        public bool IsClosed { get; set; }
        public bool IsAdminOnly { get; set; }
    }
}
