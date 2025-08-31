
using TrandingSystem.Domain.Entities;
using TrandingSystem.Domain.Interfaces;

using TrandingSystem.Infrastructure.Data;

namespace TrandingSystem.Infrastructure.Repositories
{
    public class VideoRepository : IVideoRepository
    {
        private readonly db23617Context _db;
        public VideoRepository (db23617Context context)
        {
            _db= context;
        }

        // Add New Video
        public Video Create(Video newVideo)
        {
          _db.Videos.Add(newVideo);
            return newVideo;

        }

        // delete video
        public Video Delete(int Id)
        {
            var video = _db.Videos.Find(Id);
            if(video is null)
            {
                throw new KeyNotFoundException("Not Found Video");

            }
            else
            {
                _db.Videos.Remove(video);
                return video;

            }
        }

        public void DeleteAllVideosByCourseId(List<Video> videos)
        {
            _db.Videos.RemoveRange(videos);
        }

        // For Get All Videos for a specific course
        public IQueryable<Video> GetAllVideosForCouse(int CousreId) => _db.Videos.Where(x => x.CourseId == CousreId);

        public List<Video> Read()
        {
            return _db.Videos.ToList();
        }

        public Video ReadById(int Id)
        {
            return  _db.Videos.Find(Id);
        }

        public Video Update(Video video)
        {
            var UpdatedVideo=_db.Videos.Update(video);
            return UpdatedVideo.Entity;
        }
    }

}
