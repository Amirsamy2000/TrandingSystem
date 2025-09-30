using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrandingSystem.Domain.Entities;

namespace TrandingSystem.Application.Dtos
{
    public class KycUserDto
    {
        // Personal Data
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string NationalId { get; set; }
        public DateTime JoinDate { get; set; }
        public bool IsBlocketed { get; set; }
        public bool IsConfirmed { get; set; }

        // Orders
        public List<OrdersDto> Orders { get; set; } = new();

        // Courses
        public List<CoursesUserKycDto> Courses { get; set; } = new();

        // Communities
        public List<CommunityUserKycDto> Communities { get; set; } = new();

        // Counters
        public int CountPandingOrders { get; set; }
        public int CountAcceptedOrders { get; set; }
        public int CountRejectedOrders { get; set; }

        // Total Paid
        public decimal TotalPaid { get; set; }
    }
}
