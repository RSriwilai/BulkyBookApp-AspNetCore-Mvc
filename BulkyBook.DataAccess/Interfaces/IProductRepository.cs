using BulkyBook.Models.DatabaseModel;
using BulkyBook.Models.Products;

namespace BulkyBook.DataAccess.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> CreateProduct(Product model);
        Task<List<Product>> GetAll();
        Task<Product> GetById(int? Id);
        Task Delete(int Id);
        void Update(Product model);
    }
}
