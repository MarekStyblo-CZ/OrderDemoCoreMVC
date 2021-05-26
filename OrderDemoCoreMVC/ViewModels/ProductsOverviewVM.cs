using OrderDemoCoreMVC.Models.DbSets;
using System.Collections.Generic;

namespace OrderDemoCoreMVC.ViewModels
{
    public class ProductsOverviewVM
    {
        public IEnumerable<Product> Products;
        public ProductsOverviewVM(IEnumerable<Product> products)
        {
            this.Products = products;
        }
    }
}
