using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrandingSystem.Domain.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        IVideoRepository Videos { get; }
        ICategoryRepository Categories { get; }
        ICourseRepository Courses { get; }
        IRoleRepository Roles { get; }
        IUserRepository Users { get; }
        IOrdersEnorllment ordersEnorllment { get; }
        ILiveSessionRepositry LiveSessionRepositry { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
