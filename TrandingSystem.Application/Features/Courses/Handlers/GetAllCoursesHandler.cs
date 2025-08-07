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
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;


    public GetAllCoursesHandler(IUnitOfWork unitOfWork, IUserRepository userRepository, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Response<List<CourseDto>>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var user = _unitOfWork.Users.ReadById(request.UserId);

            if (user == null && request.UserId!=0)
            {
                return Response<List<CourseDto>>.ErrorResponse("User not found", null, HttpStatusCode.NotFound);
            }

            List<Course> courses;

            if (request.UserId == 0 || user.Role?.RoleName.ToLower() == "admin")
            {
                courses = _unitOfWork.Courses.Read();
            }
            else if (user.Role?.RoleName.ToLower() == "lecturer")
            {
                courses = _unitOfWork.Courses.GetCoursesByLecturerId(request.UserId);
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
