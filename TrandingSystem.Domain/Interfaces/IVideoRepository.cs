
using TrandingSystem.Domain.Abstractions;
using TrandingSystem.Domain.Entities;
namespace TrandingSystem.Domain.Interfaces
{
    public interface IVideoRepository:IDomainInterface<Video>
    {
        // 0 get all videos for this course
        // 1 get  Just all Active Videos
        // 2 get just all unActive Videos
        IQueryable<Video> GetAllVideosForCouse(int CousreId,int Status);
       
      
        void DeleteAllVideosByCourseId(List<Video> videos);

    }
}
