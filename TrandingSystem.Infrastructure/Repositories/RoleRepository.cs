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
    public class RoleRepository : IRoleRepository
    {
        private readonly db23617Context _context;
        public RoleRepository(db23617Context context)
        {
            _context = context;
        }
        public Role Create(Role Object)
        {
            throw new NotImplementedException();
        }

        public Role Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public List<Role> getUserRole(int UserId)
        {
            return _context.Roles
            .Where(r => _context.UserRoles.Any(ur => ur.UserId == UserId && ur.RoleId == r.Id))
            .ToList();
            //return _context.Roles.Where(r => _context.UserRoles.Where(u => u.UserId == UserId).Select(ur => ur.RoleId).Contains(r.Id)).ToList();

        }

        public List<Role> Read()
        {
            throw new NotImplementedException();
        }

        public Role ReadById(int Id)
        {
            throw new NotImplementedException();
        }

        public Role Update(Role Element)
        {
            throw new NotImplementedException();
        }
    }
}
