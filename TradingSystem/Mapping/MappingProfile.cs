using AutoMapper;
using TrandingSystem.Application.Dtos;
using TrandingSystem.Application.Features.Courses.Commands;
using TrandingSystem.Domain.Entities;
using TrandingSystem.ViewModels;

namespace TrandingSystem.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Maps Presentation → Application
            CreateMap<CourseVM, AddCourseCommand>().ReverseMap();
            CreateMap<Course, AddCourseCommand>().ReverseMap();
            CreateMap<Course, CourseVM>().ReverseMap();
            CreateMap<Course, CourseDto>().ReverseMap();


        }
    }
}
