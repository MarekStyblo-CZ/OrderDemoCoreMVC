using OrderDemoCoreMVC.Data;
using OrderDemoCoreMVC.Models.DbSets;
using OrderDemoCoreMVC.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace OrderDemoCoreMVC.Services
{
    public class ProductService
    {
        private readonly SqlDbContext _db;

        public ProductService(SqlDbContext dbContext)
        {
            _db = dbContext;
        }

        public IEnumerable<Product> GetAll()
        {
            using (_db)
            {
                return _db.Products;
            }
        }

        public Product CheckCode(int code)
        {
            using (_db)
            {
                return _db.Products.FirstOrDefault(p => p.Code == code);
            }
        }

        public Product CheckCodeBeforeUpdate(int code, int productId)
        {
            using (_db)
            {
                return _db.Products.FirstOrDefault(p => p.Code == code && p.Id != productId);
            }
        }

        public void Create(ProductVM productVm)
        {
            using (_db)
            {
                _db.Products.Add(new Product { Code = productVm.Code, Name = productVm.Name, Price = productVm.Price });
                _db.SaveChanges();
            }
        }

        public Product Get(int productId)
        {
            using (_db)
            {
                return _db.Products.FirstOrDefault(p => p.Id == productId);
            }
        }

        public void Delete(int productId)
        {
            using (_db)
            {
                var foundProduct = this.Get(productId);
                _db.Products.Remove(foundProduct);
                _db.SaveChanges();
            }
        }

        public void Update(ProductVM productVm)
        {
            using (_db)
            {
                var productToEdit = this.Get(productVm.Id);
                productToEdit.Code = productVm.Code;
                productToEdit.Name = productVm.Name;
                productToEdit.Price = productVm.Price;
                _db.SaveChanges();
            }
        }


    }
}
