using BulkyBook.DataAccess.DatabaseModel;
using BulkyBook.DataAccess.DBContext;
using BulkyBook.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BulkyBookDbContext _db;

        public CategoryRepository(BulkyBookDbContext db)
        {
            _db = db;
        }

        public async Task<Category> CreateCategory(Category model)
        {
            await _db.Categories.AddAsync(model);
            await _db.SaveChangesAsync();
            
            return model;
        }

        public async Task Delete(int categoryId)
        {
            var category = await _db.Categories.FindAsync(categoryId);
            _db.Categories.Remove(category);

            await _db.SaveChangesAsync();
        }

        public async Task<List<Category>> GetAll()
        {
            var categories = await _db.Categories.ToListAsync();

            return categories;
        }

        public async Task<Category> GetById(int? categoryId)
        {
            var category = await _db.Categories.FindAsync(categoryId);

            return category;
        }

        public void Update(Category model)
        {
            _db.Categories.Update(model);
            _db.SaveChanges();
        }
    }
}
