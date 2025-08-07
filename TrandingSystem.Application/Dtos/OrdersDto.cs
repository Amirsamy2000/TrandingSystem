using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrandingSystem.Domain.Entities;

namespace TrandingSystem.Application.Dtos
{
    public class OrdersDto
    {
        public int EnrollmentId { get; set; }

        //public int UserId { get; set; }

        //public int CourseId { get; set; }

        public DateTime EnrollmentDate { get; set; }

        public bool IsConfirmed { get; set; }

        // Merged fields from Order:
        public string ReceiptImagePath { get; set; }
        public byte? OrderStatus { get; set; }   // nullable to allow missing data
        public int? ConfirmedBy { get; set; }
        public DateTime? CreatedAt { get; set; } // preserves the original Order.CreatedAt

      public string UserName { get; set; }
      public string UserEmail { get; set; }
      public string UserMobile { get; set; }
      public string CourseName { get; set; }
      public  decimal CostCourse { get; set; }
      public bool? IsPaid {  get; set; }





    }
}
