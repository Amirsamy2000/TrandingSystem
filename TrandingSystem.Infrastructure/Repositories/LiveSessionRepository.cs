using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrandingSystem.Domain.Entities;
using TrandingSystem.Domain.Interfaces;
using TrandingSystem.Infrastructure.Data;

namespace TrandingSystem.Infrastructure.Repositories
{
    public class LiveSessionRepository : ILiveSessionRepositry
    {

        private readonly db23617Context _db;
        public LiveSessionRepository(db23617Context context)
        {
            _db = context;
        }
        public LiveSession Create(LiveSession Object)
        {
            throw new NotImplementedException();
        }

        public LiveSession Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public List<LiveSession> Read()
        {
            return _db.LiveSessions.ToList();
        }

        public LiveSession ReadById(int Id)
        {
            throw new NotImplementedException();
        }

        public LiveSession Update(LiveSession Element)
        {
            throw new NotImplementedException();
        }

        public IQueryable<LiveSession> GetAllLiveSessionsForCouse(int courseId)
        {
            return _db.LiveSessions.Where(x => x.CourseId == courseId);
        }
    }
}
