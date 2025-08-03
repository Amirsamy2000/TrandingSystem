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
    public class VideoRepository : IVideoRepository
    {
        private readonly db23617Context _db;
        public VideoRepository (db23617Context context)
        {
            _db= context;
        }

        // Add New Video
        public Video Create(Video Object)
        {
            throw new KeyNotFoundException();
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
            throw new NotImplementedException();
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
