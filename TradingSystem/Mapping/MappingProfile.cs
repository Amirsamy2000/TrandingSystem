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

            CreateMap<CourseVM, UpdateCourseCommand>().ReverseMap();
            CreateMap<Course, UpdateCourseCommand>().ReverseMap();

            CreateMap<Course, CourseVM>().ReverseMap();
            // Course mapping with CategoryName
            CreateMap<Course, CourseDto>()
            .ForMember(dest => dest.categoryName, opt => opt.MapFrom(src => src.Category.CategoryNameEn))
            .ReverseMap()
            .ForMember(dest => dest.Category, opt => opt.Ignore()); // prevent reverse confusion


            CreateMap<Video_CourseEnrollment, CourseEnrollmentDto>()
            .ReverseMap();


            CreateMap<User, UserDto>()
           .ReverseMap();
            
            CreateMap<Role, RoleDto>()
           .ReverseMap();



            CreateMap<Category, CategoryDto>().ReverseMap();
            // Add other mappings here as needed
            // Map Dto ==> VideoDto to Video;
            CreateMap<Video, VideoDto>()
          .ForMember(dest => dest.Title, opt => opt.MapFrom((src, dest, _, context) =>
              context.Items["culture"].ToString() == "ar" ? src.TitleAR : src.TitleEN))
          .ForMember(dest => dest.Description, opt => opt.MapFrom((src, _, _, context) =>
              context.Items["culture"].ToString() == "ar" ? src.DescriptionAR : src.DescriptionEN));

            CreateMap<Video, ViedoUpdateDto>().ReverseMap();

            //  Map LiveSession to LiveSessionDto
            CreateMap<LiveSession, LiveSessionsDto>()
        .ForMember(dest => dest.Title, opt => opt.MapFrom((src, dest, _, context) =>
            context.Items["culture"].ToString() == "ar" ? src.TitleAR : src.TitleEN))
        .ForMember(dest => dest.Description, opt => opt.MapFrom((src, _, _, context) =>
            context.Items["culture"].ToString() == "ar" ? src.DescriptionAR : src.DescriptionEN));

            CreateMap<Community, CommunitiesDto>().ReverseMap();

        }
    }
}
