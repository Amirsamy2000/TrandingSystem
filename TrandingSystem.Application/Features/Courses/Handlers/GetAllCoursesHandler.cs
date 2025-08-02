using AutoMapper;
using MediatR;
using System.Net;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Dtos;
using TrandingSystem.Application.Features.Courses.Queries;
using TrandingSystem.Domain.Entities;
using TrandingSystem.Domain.Interfaces;

internal class GetAllCoursesHandler : IRequestHandler<GetAllCoursesQuery, Response<List<CourseDto>>>
{
    private readonly ICourseRepository _courseRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetAllCoursesHandler(ICourseRepository courseRepository, IUserRepository userRepository, IMapper mapper)
    {
        _courseRepository = courseRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Response<List<CourseDto>>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var user = _userRepository.ReadById(request.UserId);

            if (user == null)
            {
                return Response<List<CourseDto>>.ErrorResponse("User not found", null, HttpStatusCode.NotFound);
            }

            List<Course> courses;

            if (user.Role?.RoleName.ToLower() == "admin")
            {
                courses = _courseRepository.Read();
            }
            else if (user.Role?.RoleName.ToLower() == "lecturer")
            {
                courses = _courseRepository.GetCoursesByLecturerId(request.UserId);
            }
            else
            {
                return Response<List<CourseDto>>.ErrorResponse("Unauthorized role", null, HttpStatusCode.Forbidden);
            }

            if (courses == null || !courses.Any())
            {
                return Response<List<CourseDto>>.ErrorResponse("No courses found", null, HttpStatusCode.NotFound);
            }

            var courseDtos = _mapper.Map<List<CourseDto>>(courses);
            return Response<List<CourseDto>>.SuccessResponse(courseDtos);
        }
        catch (Exception ex)
        {
            return Response<List<CourseDto>>.ErrorWithException("An error occurred", ex.Message);
        }
    }
}
