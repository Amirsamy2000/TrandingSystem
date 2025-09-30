using Amazon.S3.Model;
using MailKit.Search;
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

        public DateTime? EnrollmentDate { get; set; }

        public bool IsConfirmed { get; set; }

        // Merged fields from Order:
        public string ReceiptImagePath { get; set; }
        public byte? OrderStatus { get; set; }   // nullable to allow missing data
        public int? ConfirmedBy { get; set; }
        public DateTime? CreatedAt { get; set; } // preserves the original Order.CreatedAt
        public string CashPhoneNum { get; set; }


        public string UserName { get; set; }
      public string UserEmail { get; set; }
      public string UserMobile { get; set; }
      public string CourseName { get; set; }
      public  decimal CostCourse { get; set; }
      public bool? IsPaid {  get; set; }
        public string? VideoName { set; get; }
        public string LiveName { set; get; }
        public decimal VideoCost { set; get; } = 0;
        public decimal LiveCost { set; get; } = 0;

        public int OrderByType {  get; set; } //0 = course ,1 = video ,2 = live






    }
}
