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
    internal class CourseRepository : ICourseRepository
    {
        private readonly db23617Context _context;
        public Course Create(Course Object)
        {
            
            _context.Courses.Add(Object);
            _context.SaveChanges();
            return Object;
           

        }

        public List<Course> Read()
        {
            throw new NotImplementedException();
        }

        public Course ReadById(int Id)
        {
            throw new NotImplementedException();
        }

        public Course Update(Course Element)
        {
            throw new NotImplementedException();
        }
        public Course Delete(int Id)
        {
            throw new NotImplementedException();
        }

    }
}
