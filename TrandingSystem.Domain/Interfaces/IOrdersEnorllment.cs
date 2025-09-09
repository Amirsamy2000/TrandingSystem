using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TrandingSystem.Domain.Abstractions;
using TrandingSystem.Domain.Entities;

namespace TrandingSystem.Domain.Interfaces
{
    public interface IOrdersEnorllment:IDomainInterface<Video_CourseEnrollment>
    {
        IEnumerable<Video_CourseEnrollment> GetEnrollmentsByVideoId(int VideoId);
        bool DeleteRange(IEnumerable<Video_CourseEnrollment> enrollments);

        Video_CourseEnrollment? CheckUserEnrollment(Expression<Func<Video_CourseEnrollment, bool>> condition);

    }
}
