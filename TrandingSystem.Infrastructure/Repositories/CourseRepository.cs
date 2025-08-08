using Microsoft.EntityFrameworkCore;
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
    public class CourseRepository : ICourseRepository
    {
        private readonly db23617Context _context;

        public CourseRepository(db23617Context context)
        {
            _context = context;
        }
        public Course Create(Course Object)
        {
            
            _context.Courses.Add(Object);
            _context.SaveChanges();
            return Object;
           

        }

        public List<Course> Read()
        {
            return _context.Courses.ToList();
        }

        public List<Course> GetCoursesByLecturerId(int lecturerId)
        {
            return _context.CourseLecturers
                .Include(cl => cl.Course)
                    .ThenInclude(c => c.Category)
                .Where(cl => cl.LecturerId == lecturerId)
                .Select(cl => cl.Course)
                .ToList();

        }

        public Course ReadById(int Id)
        {
            return _context.Courses.Find(Id);
        }

        public Course Update(Course Element)
        {
            throw new NotImplementedException();
        }
        public Course Delete(int Id)
        {
            var course = _context.Courses.Find(Id);
            _context.Courses.Remove(course);
            _context.SaveChanges();
            return course;
        }

        public bool IsCourseEnrolled(int courseId, int userId)
        {
            return _context.CourseEnrollments
                .Any(ce => ce.CourseId == courseId && ce.UserId == userId);
        }

        public bool EnrollCourse(int courseId, int userId, string RecieptUrl)
        {

            var courseEnrol = new CourseEnrollment
            {
                CourseId = courseId,
                UserId = userId,
                CreatedAt = DateTime.Now,
                IsConfirmed = false,
                ReceiptImagePath = RecieptUrl,
                OrderStatus = 2, // Assuming OrderStatus is nullable
            };


            _context.CourseEnrollments.Add(courseEnrol);

            _context.SaveChanges();
            return true;
        }
    }
}
