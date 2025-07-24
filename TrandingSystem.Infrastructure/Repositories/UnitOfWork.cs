using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrandingSystem.Domain.Interfaces;
using TrandingSystem.Infrastructure.Data;

namespace TrandingSystem.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly db23617Context _context;
        private IVideoRepository _videos;
        public IVideoRepository Videos => _videos ??= new VideoRepository(_context);


        public UnitOfWork(db23617Context context)
        {
            _context = context;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => await _context.SaveChangesAsync(cancellationToken);
        public void Dispose()
        {
            _context.Dispose(); // تأكد إنك بتقفل الكونكشن مع قاعدة البيانات
        }
    }
}
