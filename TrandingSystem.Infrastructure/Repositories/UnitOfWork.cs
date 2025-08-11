using System;
using System.Threading;
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

        private ICategoryRepository _categories;
        public ICategoryRepository Categories => _categories ??= new CategoryRepository(_context);

        private ICourseRepository _courses;
        public ICourseRepository Courses => _courses ??= new CourseRepository(_context);

        private IUserRepository _Users;
        public IUserRepository Users => _Users ??= new UserRepository(_context);

        private IOrdersEnorllment _ordersEnorllment;
        public IOrdersEnorllment ordersEnorllment => _ordersEnorllment ??= new OrdersEnorllment(_context);

        private ILiveSessionRepositry _liveSession;
        public ILiveSessionRepositry LiveSessionRepositry => _liveSession ??= new LiveSessionRepository(_context);
        public UnitOfWork(db23617Context context)
        {
            _context = context;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _context.Dispose(); // Ensures the DB context is properly disposed
        }
    }
}
