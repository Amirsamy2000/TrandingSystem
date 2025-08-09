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
    public class OrdersEnorllment : IOrdersEnorllment
    {
        private readonly db23617Context _db;
        public OrdersEnorllment(db23617Context context)
        {
            _db = context;
        }
        public CourseEnrollment Create(CourseEnrollment Object)
        {
            throw new NotImplementedException();
        }

        public CourseEnrollment Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public List<CourseEnrollment> Read()
        {
           return _db.CourseEnrollments.ToList();
        }

        public CourseEnrollment ReadById(int Id)
        {
            return _db.CourseEnrollments.Where(x=>x.EnrollmentId==Id).FirstOrDefault();
        }

        public CourseEnrollment Update(CourseEnrollment Element)
        {
            _db.CourseEnrollments.Update(Element);
            return Element;
        }
    }
}
