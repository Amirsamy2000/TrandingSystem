using Microsoft.AspNetCore.Http;

namespace TrandingSystem.Application.Dtos
{
    public class ViedoUpdateDto
    {
        public int VideoId { get; set; }
        public int CourseId { get; set; }

        public string? TitleEN { get; set; }
        public string? TitleAR { get; set; }

        
        public string? DescriptionEN { get; set; }
        public string? DescriptionAR { get; set; }

       
        public bool IsPaid { get; set; }

        public decimal Cost { get; set; }

        public DateTime CreatedAt { get; set; }
        public int? CreadteBy { get; set; }

        public bool IsActive { get; set; }
        public IFormFile? ImageVideoUrlNew { get; set; }
       
    }
}
