using AutoMapper;
using TrandingSystem.Domain.Entities;
using TrandingSystem.Application.Dtos;

namespace TrandingSystem.Application.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Course, CourseDto>().ReverseMap();
            // Add other mappings here as needed
            // Map Dto ==> VideoDto to Video;
            CreateMap<Video, VideoDto>()
          .ForMember(dest => dest.Title, opt => opt.MapFrom((src, dest, _, context) =>
              context.Items["culture"].ToString() == "ar" ? src.TitleAR : src.TitleEN))
          .ForMember(dest => dest.Description, opt => opt.MapFrom((src, _, _, context) =>
              context.Items["culture"].ToString() == "ar" ? src.DescriptionAR : src.DescriptionEN));
        }
    }
}
