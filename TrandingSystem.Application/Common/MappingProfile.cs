using AutoMapper;
using TrandingSystem.Application.Dtos;
using TrandingSystem.Application.Features.Courses.Commands;
using TrandingSystem.Domain.Entities;

namespace TrandingSystem.Application.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            

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
