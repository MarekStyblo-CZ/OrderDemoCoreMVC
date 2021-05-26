using Microsoft.AspNetCore.Mvc;
using OrderDemoCoreMVC.Data;
using OrderDemoCoreMVC.Services;
using OrderDemoCoreMVC.ViewModels;

namespace OrderDemoCoreMVC.Controllers
{
    public class ProductsController : Controller
    {
        private ProductService _productService;

        public ProductsController(SqlDbContext sqlDbContext)
        {
            this._productService = new ProductService(sqlDbContext);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new ProductsOverviewVM(_productService.GetAll()));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new ProductVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductVM vm)
        {
            if (_productService.CheckCode(vm.Code) != null)
                this.ModelState.AddModelError("Code", "Zadaný kód již existuje - je třeba zadat unikátní kód");

            if (this.ModelState.IsValid)
            {
                _productService.Create(vm);
                return RedirectToAction("Index");
            }
            else
                return View(vm);
        }

        // Probably better user experience would be 2 level confirmation of item delete (1st show detailed item info -> 2nd delete)
        //#todo Another think is that product is restricted to be deleted when it is on OrderItem - would be nice to show better msg 

        [HttpGet]
        public IActionResult Delete(int productId)
        {
            _productService.Delete(productId);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int productId)
        {
            var foundProduct = _productService.Get(productId);
            return View(new ProductVM { Id = foundProduct.Id, Code = foundProduct.Code, Name = foundProduct.Name, Price = foundProduct.Price });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(ProductVM vm)
        {
            if (_productService.CheckCodeBeforeUpdate(vm.Code, vm.Id) != null)
                this.ModelState.AddModelError("Code", "Zadaný kód již existuje - je třeba zadat unikátní kód");

            if (this.ModelState.IsValid)
            {
                _productService.Update(vm);
                return RedirectToAction("Index");
            }
            else
                return View(vm);
        }
    }
}
