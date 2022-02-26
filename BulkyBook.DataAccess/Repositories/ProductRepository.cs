using BulkyBook.DataAccess.DatabaseModel;
using BulkyBook.DataAccess.DBContext;
using BulkyBook.DataAccess.Interfaces;
using BulkyBook.Models.Product;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly BulkyBookDbContext _db;

        public ProductRepository(BulkyBookDbContext db)
        {
            _db = db;
        }

        public async Task<Product> CreateCoverType(Product model)
        {
            await _db.Products.AddAsync(model);
            await _db.SaveChangesAsync();
            return model;
        }

        public async Task Delete(int Id)
        {
            var product = await _db.Products.FindAsync(Id);
            _db.Products.Remove(product);
        }

        public async Task<List<Product>> GetAll()
        {
            var products = await _db.Products.ToListAsync();

            return products;
        }

        public async Task<Product> GetById(int? Id)
        {
            var product = await _db.Products.FindAsync(Id);

            return product;
        }

        public void Update(Product model)
        {
            var obj = _db.Products.FirstOrDefault(u => u.ProductId == model.ProductId);
            if(obj != null)
            {
                obj.Title = model.Title;
                obj.ISBN = model.ISBN;
                obj.Price = model.Price;
                obj.Price50 = model.Price50;
                obj.Price100 = model.Price100;
                obj.ListPrice = model.ListPrice;
                obj.Description = model.Description;
                obj.CategoryId = model.CategoryId;
                obj.Author = model.Author;
                obj.CoverType = model.CoverType;
                if(obj.ImageUrl != null)
                {
                    obj.ImageUrl = model.ImageUrl;
                }
            }
            _db.Update(model);
            _db.SaveChanges();
        }
    }
}
