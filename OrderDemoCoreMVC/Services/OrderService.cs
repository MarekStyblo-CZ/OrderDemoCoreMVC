using OrderDemoCoreMVC.Data;
using OrderDemoCoreMVC.Models.DbSets;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OrderDemoCoreMVC.ViewModels;

namespace OrderDemoCoreMVC.Services
{
    public class OrderService
    {
        private readonly SqlDbContext _db;

        public OrderService(SqlDbContext dbContext)
        {
            _db = dbContext;
        }

        public IEnumerable<Order> GetAll()
        {
            using (_db)
            {
                return _db.Orders.Include(o => o.OderItems);
            }
        }

        public Order Get(int orderId)
        {
            using (_db)
            {
                return _db.Orders
                    .Include(o => o.OderItems)
                    .FirstOrDefault(o => o.Id == orderId);
            }
        }

        //#todo refactor
        public void Create(OrderVM orderVm, ProductService productService)
        {
            using (_db)
            {
                var order = new Order { CustomerName = orderVm.CustomerName, CustomerAddress = orderVm.CustomerAddress, Created = orderVm.Created, OderItems = new List<OrderItem>() };
                var selectedProducts = orderVm.SelectedProducts
                    .Where(i => i.Quantity > 0)
                    .ToList();

                foreach (var selectedProduct in selectedProducts)
                {
                    var product = productService.Get(selectedProduct.Id);
                    order.OderItems.Add(new OrderItem { Order = order, Product = product, Price = product.Price, Quantity = selectedProduct.Quantity });
                };
                _db.Orders.Add(order);
                _db.SaveChanges();
            }
        }

        public void Delete(int orderId)
        {
            using (_db)
            {
                var foundOrder = this.Get(orderId);
                _db.Orders.Remove(foundOrder);
                _db.SaveChanges();
            }
        }

    }
}
