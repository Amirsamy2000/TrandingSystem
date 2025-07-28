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

        public Video Create(Video Object)
        {
            throw new NotImplementedException();
        }

        public Video Delete(int Id)
        {
            throw new NotImplementedException();
        }

        // For Get All Videos for a specific course
        public IQueryable<Video> GetAllVideosForCouse(int CousreId) => _db.Videos.Where(x => x.CourseId == CousreId);

        public List<Video> Read()
        {
            throw new NotImplementedException();
        }

        public Video ReadById(int Id)
        {
            throw new NotImplementedException();
        }

        public Video Update(Video Element)
        {
            throw new NotImplementedException();
        }
    }

}
