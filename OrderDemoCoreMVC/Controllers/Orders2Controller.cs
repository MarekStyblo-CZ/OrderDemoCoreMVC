using Microsoft.AspNetCore.Mvc;
using OrderDemoCoreMVC.Data;
using OrderDemoCoreMVC.Services;
using OrderDemoCoreMVC.ViewModels;
using System.Linq;

namespace OrderDemoCoreMVC.Controllers
{
    public class Orders2Controller : Controller
    {
        private SqlDbContext _sqlDbContext;
        private OrderService _orderService;
        private OrderItemService _orderItemService;
        private ProductService _productService;

        public Orders2Controller(SqlDbContext sqlDbContext)
        {
            this._sqlDbContext = sqlDbContext;
            this._orderService = new OrderService(sqlDbContext);
            this._orderItemService = new OrderItemService(sqlDbContext);
            this._productService = new ProductService(sqlDbContext);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new OrdersOverviewVM(_orderService.GetAll()));
        }

        [HttpGet]
        public IActionResult Detail(int orderId)
        {
            return View(new OrderVM(_orderItemService.Get(orderId)));
        }

        // To make it simple - I allow to create a new order with maximum of 10 order items - to avoid need for JS..
        [HttpGet]
        public IActionResult Create()
        {
            return View(new OrderVM(_productService));
        }

        [HttpPost]
        public IActionResult Create(OrderVM vm)
        {
            if (vm.SelectedProducts.FirstOrDefault(p => p.Quantity > 0) == null)
                this.ModelState.AddModelError("OrderItemErr", "Je třeba vybrat alespoň jeden produkt (nenulového množství)");

            if (vm.SelectedProducts.FirstOrDefault(p => p.Quantity < 0) != null)
                this.ModelState.AddModelError("OrderItemErr", "Množství mohou být jen kladná čísla");

            if (this.ModelState.IsValid)
            {
                _orderService.Create(vm, _productService);
                return RedirectToAction("Index");
            }
            else
            {
                vm.InitData(_productService); //data comming from
                return View(vm);
            }
        }

        // Probably better user experience would be 2 level confirmation of item delete (1st show detailed item info -> 2nd delete)

        [HttpGet]
        public IActionResult Delete(int orderId)
        {
            _orderService.Delete(orderId);
            return RedirectToAction("Index");
        }
    }
}
