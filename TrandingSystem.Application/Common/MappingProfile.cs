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
        }
    }
}
