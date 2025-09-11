using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrandingSystem.Domain.Abstractions;
using TrandingSystem.Domain.Entities;

namespace TrandingSystem.Domain.Interfaces
{
    public interface ICategoryRepository : IDomainInterface<Category>
    {
        Category GetCategoryByName(string ENName, string ARName);
    }
}
