using BulkyBook.DataAccess.DatabaseModel;
using BulkyBook.Models.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Interfaces
{
    public interface ICategoryRepository
    {
        Task<CategoryDto> CreateCategory(CategoryDto model);
        Task<List<Category>> GetAll();
        Task<Category> GetById(int? categoryId);
        Task Delete(int categoryId);
        void Update(Category model);
    }
}
