
using TrandingSystem.Domain.Abstractions;
using TrandingSystem.Domain.Entities;
namespace TrandingSystem.Domain.Interfaces
{
    public interface IVideoRepository:IDomainInterface<Video>
    {
         IQueryable<Video> GetAllVideosForCouse(int CousreId);
       
        void DeleteAllVideosByCourseId(List<Video> videos);

    }
}
