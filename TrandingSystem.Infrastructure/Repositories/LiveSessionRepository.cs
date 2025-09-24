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
            _db.LiveSessions.Add(Object);
            return Object;
        }

        public LiveSession Delete(int Id)
        {
            var session = _db.LiveSessions.Find(Id);
            if (session != null)
            {
                _db.LiveSessions.Remove(session);
                return session;
            }
            return null;

        }

        public List<LiveSession> Read()
        {
            return _db.LiveSessions.ToList();
        }

        public LiveSession ReadById(int Id)
        {
            return _db.LiveSessions.Find(Id);
        }

        public LiveSession Update(LiveSession Element)
        {
            _db.LiveSessions.Update(Element);
            return Element;
        }

        public IQueryable<LiveSession> GetAllLiveSessionsForCouse(int courseId, int Status)
        {
            if (Status == 0)
            {
                return _db.LiveSessions.Where(x => x.CourseId == courseId);
            }
            else if (Status == 1)
            {
                return _db.LiveSessions.Where(x => x.CourseId == courseId && x.IsActive == true);
            }
            else
                return _db.LiveSessions.Where(x => x.CourseId == courseId&& x.IsActive == false);
        }

        public void DeleteAllSessionsByCourseId(List<LiveSession> liveSessions)
        {
            _db.LiveSessions.RemoveRange(liveSessions);
        }
    }
}
