using BulkyBook.DataAccess.DatabaseModel;
using BulkyBook.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> CreateCoverType(Product model);
        Task<List<Product>> GetAll();
        Task<Product> GetById(int? Id);
        Task Delete(int Id);
        void Update(Product model);
    }
}
