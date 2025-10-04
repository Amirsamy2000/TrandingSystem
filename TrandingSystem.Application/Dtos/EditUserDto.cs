using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrandingSystem.Application.Dtos
{
    public class EditUserDto
    {
        public int Id { get; set; }

        //[Required(ErrorMessage = "Email is required")]
        //[EmailAddress(ErrorMessage = "Invalid Email format")]
        //public string Email { get; set; }

        [Phone(ErrorMessage = "Invalid phone number")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "Phone number must be between 10 and 15 digits")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Full name is required")]
        [StringLength(100, ErrorMessage = "Full name cannot exceed 100 characters")]
        public string FullName { get; set; }

        [StringLength(14, ErrorMessage = "National ID cannot exceed 14 digits")]
        public string NationalId { get; set; }
     
    }
}
