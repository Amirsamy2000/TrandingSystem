
using TrandingSystem.Domain.Abstractions;
using TrandingSystem.Domain.Entities;

namespace TrandingSystem.Domain.Interfaces
{
    public interface IUserRepository : IDomainInterface<User>
    {
        List<User> GetActiveAndConfirmUser();
        List<User> GetUserEnrollInCourse(int CourseId);

    }
}
