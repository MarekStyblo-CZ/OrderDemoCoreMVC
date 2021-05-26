using Microsoft.EntityFrameworkCore;
using OrderDemoCoreMVC.Data;
using OrderDemoCoreMVC.Models.DbSets;
using System.Collections.Generic;
using System.Linq;

namespace OrderDemoCoreMVC.Services
{
    public class OrderItemService
    {
        private readonly SqlDbContext _db;

        public OrderItemService(SqlDbContext dbContext)
        {
            _db = dbContext;
        }

        public List<OrderItem> Get(int orderId)
        {
            using (_db)
            {
                return _db.OrderItems
                    .Include(oi => oi.Order)
                    .Include(oi => oi.Product)
                    .Where(o => o.OrderId == orderId)
                    .ToList();
            }
        }

        public List<OrderItem> GetAll()
        {
            using (_db)
            {
                return _db.OrderItems
                    .Include(oi => oi.Order)
                    .Include(oi => oi.Product)
                    .ToList();
            }
        }
    }
}
