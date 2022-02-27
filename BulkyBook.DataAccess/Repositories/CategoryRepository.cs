using BulkyBook.DataAccess.DBContext;
using BulkyBook.DataAccess.Interfaces;
using BulkyBook.Models.Category;
using BulkyBook.Models.DatabaseModel;
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

        public async Task<CategoryDto> CreateCategory(CategoryDto model)
        {
            var category = new Category
            {
                Name = model.Name,
                DisplayOrder = model.DisplayOrder
            };
            await _db.Categories.AddAsync(category);
            await _db.SaveChangesAsync();
            
            return model;
        }

        public async Task Delete(int categoryId)
        {
            var category = await _db.Categories.FindAsync(categoryId);
            _db.Categories.Remove(category);

            await _db.SaveChangesAsync();
        }

        public IEnumerable<Category> GetAll()
        {
            var categories = _db.Categories.ToList();

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
