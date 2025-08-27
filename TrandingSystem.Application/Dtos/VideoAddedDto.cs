using Microsoft.AspNetCore.Http;


namespace TrandingSystem.Application.Dtos
{
    public class VideoAddedDto
    {
        public int CourseId { get; set; }

        public string TitleEN { get; set; }
        public string TitleAR { get; set; }


        public string DescriptionEN { get; set; }
        public string DescriptionAR { get; set; }


        public bool IsPaid { get; set; }

        public decimal Cost { get; set; } = 0;

        public DateTime CreatedAt { get; set; }
        public int? CreadteBy { get; set; }

        public bool IsActive { get; set; }
        public IFormFile ImageVideoUrl { get; set; }

        public string VideoUrl { get; set; }
        public bool notfiy { set; get; }
    }
}
