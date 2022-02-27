using BulkyBook.Models.Categories;
using BulkyBook.Models.DatabaseModel;

namespace BulkyBook.DataAccess.Interfaces
{
    public interface ICategoryRepository
    {
        Task<CategoryDto> CreateCategory(CategoryDto model);
        IEnumerable<Category> GetAll();
        Task<Category> GetById(int? categoryId);
        Task Delete(int categoryId);
        void Update(Category model);
    }
}
