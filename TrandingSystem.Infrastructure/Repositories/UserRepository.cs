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
    public class UserRepository : IUserRepository
    {
        private readonly db23617Context _context;

        public UserRepository(db23617Context context)
        {
            _context = context;
        }
        public User Create(User Object)
        {
            throw new NotImplementedException();
        }

        public User Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public List<User> ReadAllTeacher()
        {
            return _context.Users
                .Where(u => _context.CourseLecturers
                    .Select(cl => cl.LecturerId)
                    .Contains(u.Id))
                .ToList();
        }

        public List<User> Read()
        {
            return _context.Users.ToList();
        }

        public User ReadById(int Id)
        {
            return _context.Users
            .Include(u => u.Role)
            .FirstOrDefault(u => u.Id == Id);
        }

        public User Update(User Element)
        {
            throw new NotImplementedException();
        }
    }
}
