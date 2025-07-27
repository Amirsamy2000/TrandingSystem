using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.Application.Common.Response;
using TrandingSystem.Application.Dtos;
using TrandingSystem.Application.Features.Courses.Queries;
using TrandingSystem.Domain.Entities;
using TrandingSystem.Domain.Interfaces;

namespace TrandingSystem.Application.Features.Courses.Handlers
{
    internal class GetAllCoursesHandler : IRequestHandler<GetAllCoursesQuery, Response<List<CourseDto>>>
    {
        private readonly ICourseRepository _repository;
        private readonly IMapper _mapper;

        public GetAllCoursesHandler(ICourseRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<Response<List<CourseDto>>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var courses = _repository.Read(); // assuming this returns List<Course>
                if (courses == null || !courses.Any())
                {
                    return Task.FromResult(Response<List<CourseDto>>.ErrorResponse("there are no courses available", null, HttpStatusCode.NotFound));

                }
                //var courseDtos = courses.Select(c => new CourseDto
                //{
                //    TitleEN = c.TitleEN,
                //    TitleAR = c.TitleAR,
                //    DescriptionEN = c.DescriptionEN,
                //    DescriptionAR = c.DescriptionAR,
                //    CategoryId = c.CategoryId,
                //    Cost = c.Cost,
                //    CommunityAutoCreate = c.CommunityAutoCreate,
                //    IsLive = c.IsLive,
                //    CreateBy = c.CreateBy,
                //    CreateAt = c.CreateAt,
                //    IsFullyFree = c.IsFullyFree,
                //    IsActive = c.IsActive,
                //    ImageCourseUrl = c.ImageCourseUrl
                //}).ToList();
                var CourseDTO = _mapper.Map<List<CourseDto>>(courses);
                return Task.FromResult(Response<List<CourseDto>>.SuccessResponse(CourseDTO));
            }
            catch(Exception ex)
            {
                return Task.FromResult(Response<List<CourseDto>>.ErrorWithException("There was an error", ex.Message));


            }

        }
    }
}
