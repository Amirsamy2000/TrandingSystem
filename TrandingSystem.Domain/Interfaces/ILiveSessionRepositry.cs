using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrandingSystem.Domain.Abstractions;
using TrandingSystem.Domain.Entities;

namespace TrandingSystem.Domain.Interfaces
{
    public interface ILiveSessionRepositry: IDomainInterface<LiveSession>
    {
        // 0 get all lives for this course
        // 1 get  Just all Active Lives
        // 2 get just all unActive Lives
        IQueryable<LiveSession> GetAllLiveSessionsForCouse(int CourseId,int status);

        void DeleteAllSessionsByCourseId(List<LiveSession> liveSessions);
    }
}
