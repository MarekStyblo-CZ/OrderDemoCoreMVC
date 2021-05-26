using OrderDemoCoreMVC.Models.DbSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderDemoCoreMVC.ViewModels
{
    public class OrdersOverviewVM
    {
        public IEnumerable<Order> Orders;

        public OrdersOverviewVM(IEnumerable<Order> orders)
        {
            this.Orders = orders;
        }
    }
}
