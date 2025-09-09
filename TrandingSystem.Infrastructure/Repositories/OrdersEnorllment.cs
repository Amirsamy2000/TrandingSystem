using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TrandingSystem.Domain.Entities;
using TrandingSystem.Domain.Interfaces;
using TrandingSystem.Infrastructure.Data;

namespace TrandingSystem.Infrastructure.Repositories
{
    public class OrdersEnorllment : IOrdersEnorllment
    {
        private readonly db23617Context _db;
        public OrdersEnorllment(db23617Context context)
        {
            _db = context;
        }
        public Video_CourseEnrollment Create(Video_CourseEnrollment Object)
        {
            throw new NotImplementedException();
        }

        public Video_CourseEnrollment Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public List<Video_CourseEnrollment> Read()
        {
           return _db.CourseEnrollments.ToList();
        }

        public Video_CourseEnrollment ReadById(int Id)
        {
            return _db.CourseEnrollments.Where(x=>x.EnrollmentId==Id).FirstOrDefault();
        }

        public Video_CourseEnrollment Update(Video_CourseEnrollment Element)
        {
            _db.CourseEnrollments.Update(Element);
            return Element;
        }

        public IEnumerable<Video_CourseEnrollment> GetEnrollmentsByVideoId(int VideoId)
        {
            return _db.CourseEnrollments.Where(e => e.VideoId == VideoId).ToList();
        }
        public bool DeleteRange(IEnumerable<Video_CourseEnrollment> enrollments)
        {
            _db.CourseEnrollments.RemoveRange(enrollments);
            return true;
        }

        // check if user is enrolled in a specific course
        // check if user is enrolled in a specific video
        // check if user is enrolled in a specific LiveSession
        public Video_CourseEnrollment? CheckUserEnrollment(Expression<Func<Video_CourseEnrollment, bool>> condition)
        {
            return _db.CourseEnrollments
                      .AsNoTracking() // تحسين الأداء لأنه مجرد قراءة
                      .FirstOrDefault(condition);
        }

    }
}
