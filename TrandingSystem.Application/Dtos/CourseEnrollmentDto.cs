using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrandingSystem.Domain.Entities;

namespace TrandingSystem.Application.Dtos
{
    public class CourseEnrollmentDto
    {

        public int EnrollmentId { get; set; }

        public int UserId { get; set; }

        public int CourseId { get; set; }

        public DateTime? EnrollmentDate { get; set; } //confirmed At 

        public bool IsConfirmed { get; set; }

        // Merged fields from Order:
        public string ReceiptImagePath { get; set; }
        public byte? OrderStatus { get; set; }   // nullable to allow missing data //0 rejected, 1 accepted , 2 pending
        public int? ConfirmedBy { get; set; }

        // Changed from byte to int to match UserId type
        public DateTime? CreatedAt { get; set; } // preserves the original Order.CreatedAt

        public string CashPhoneNum { get; set; }

    }
}
