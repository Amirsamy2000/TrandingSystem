using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrandingSystem.Domain.Abstractions;
using TrandingSystem.Domain.Entities;
using TrandingSystem.Domain.Interfaces;
using TrandingSystem.Infrastructure.Data;

namespace TrandingSystem.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly db23617Context _context;

        public CategoryRepository(db23617Context context)
        {
            _context = context;
        }

        public Category Create(Category Object)
        {
            _context.Categories.Add(Object);
            _context.SaveChanges();
            return Object;
        }

        public List<Category> Read()
        {
            return _context.Categories.ToList();
        }

        public Category Delete(int Id)
        {
            throw new NotImplementedException();
        }


        public Category ReadById(int Id)
        {
            throw new NotImplementedException();
        }

        public Category Update(Category Element)
        {
            throw new NotImplementedException();
        }
    }
}
