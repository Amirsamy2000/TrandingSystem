
using TrandingSystem.Domain.Abstractions;
using TrandingSystem.Domain.Entities;

namespace TrandingSystem.Domain.Interfaces
{
    public interface IUserRepository : IDomainInterface<User>
    {
        public List<User> ReadAllTeacher();
        List<User> GetActiveAndConfirmUser();

    }
}
