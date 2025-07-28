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
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
