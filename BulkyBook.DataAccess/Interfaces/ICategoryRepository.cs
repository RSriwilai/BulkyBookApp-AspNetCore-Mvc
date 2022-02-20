using BulkyBook.DataAccess.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category> CreateCategory(Category model);
        Task<List<Category>> GetAll();
        Task<Category> GetById(int? categoryId);
        Task Delete(int categoryId);
        void Update(Category model);
    }
}
