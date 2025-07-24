using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrandingSystem.Application.Features.Courses.Commands;
using TrandingSystem.Domain.Entities;
using TrandingSystem.Domain.Interfaces;

namespace TrandingSystem.Application.Features.Courses.Handlers
{
    internal class GetAllCoursesHandler : IRequestHandler<GetAllCoursesQuery, List<Course>>
    {
        private readonly ICourseRepository _repository;
        public GetAllCoursesHandler(ICourseRepository repository)
        {
            _repository = repository;
        }

        public Task<List<Course>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
        {
            var courses = _repository.Read(); // assuming this returns List<Course>
            return Task.FromResult(courses);
        }
    }
}
