using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrandingSystem.Domain.Entities;

namespace TrandingSystem.Domain.Interfaces
{
    public interface IUserRepository
    {
        public bool AddUser(string name, string email);

        public List<string> GetAllUsers();
    }
}
