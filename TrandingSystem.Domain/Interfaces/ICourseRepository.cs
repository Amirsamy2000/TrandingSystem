using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrandingSystem.Domain.Abstractions;
using TrandingSystem.Domain.Entities;
namespace TrandingSystem.Domain.Interfaces
{
    public interface ICourseRepository : IDomainInterface<Course>
    {
        List<Course> GetCoursesByLecturerId(int lecturerId);
        bool IsCourseEnrolled(int courseId, int userId);
        bool EnrollCourse(int courseId, int userId);


    }
}
