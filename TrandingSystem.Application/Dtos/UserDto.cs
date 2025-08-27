using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrandingSystem.Domain.Entities;

namespace TrandingSystem.Application.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Mobile { get; set; }
        public DateTime? RegisteredAt { get; set; }
        public bool IsBlocked { get; set; }
        public int RoleId { get; set; } // Changed from byte to int
        public string Address { get; set; }
        public string? Email { get; set; }
        public string? NationalId { get; set; }


    }
}
