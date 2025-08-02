using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrandingSystem.Domain.Entities;

namespace TrandingSystem.Application.Dtos
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }

        public string CategoryNameEn { get; set; }
        public string CategoryNameAr { get; set; }
        public int? CreateBy { get; set; }

        public DateTime? CreateAt { get; set; }

        public bool? IsActive { get; set; }

    }
}
