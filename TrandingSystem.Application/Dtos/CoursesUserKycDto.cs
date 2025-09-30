using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrandingSystem.Application.Dtos
{
    public class CoursesUserKycDto
    {
        public string? CourseName { get; set; }
        public decimal CourseCost { get; set; }
        public int CountVideos { get; set; }
        public int CountLives { get; set; }
        public bool CourseStatus { get; set; }
       public DateTime CreateAt { get; set; }
         
    }

    public class CommunityUserKycDto
    {
        public string? CommunityName { get; set; }
        public DateTime JoinDate { get; set; }
        public bool CommunityStatus { get; set; }

        public bool UserStatus { get; set; }
        public string? CourseName { get; set; }

    }
}
