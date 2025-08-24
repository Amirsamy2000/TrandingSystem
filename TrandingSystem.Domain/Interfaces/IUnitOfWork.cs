

namespace TrandingSystem.Domain.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        IVideoRepository Videos { get; }
        ICategoryRepository Categories { get; }
        ICourseRepository Courses { get; }
        IUserRepository Users { get; }
        IOrdersEnorllment ordersEnorllment { get; }
        ILiveSessionRepositry LiveSessionRepositry { get; }
        ICommunityRepository Communities { get; }
        ICommunityMemberRepository CommunityMember  { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
