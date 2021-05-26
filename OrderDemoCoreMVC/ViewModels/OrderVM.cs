using Microsoft.AspNetCore.Mvc.Rendering;
using OrderDemoCoreMVC.Models.DbSets;
using OrderDemoCoreMVC.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace OrderDemoCoreMVC.ViewModels
{
    public class OrderVM
    {
        [DisplayName("Id")]
        public int Id { get; set; }

        [DisplayName("Jméno zákazníka")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Je třeba zadat jméno zákazníka")]
        public string CustomerName { get; set; }

        [DisplayName("Adresa zákazníka")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Je třeba zadat adresu zákazníka")]
        public string CustomerAddress { get; set; }

        [DisplayName("Datum")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime Created { get; set; }

        public List<OrderItem> OrderItems { get; set; }

        public List<SelectListItem> ProductOptions { get; set; }
        public List<SelectedProduct> SelectedProducts { get; set; }

        public string OrderItemErr { get; set; }


        public OrderVM()
        {

        }

        public OrderVM(ProductService productService)
        {
            Created = DateTime.Now;

            InitData(productService);

            SelectedProducts = new List<SelectedProduct>();
            for (int i = 0; i < 10; i++)
            {
                SelectedProducts.Add(new SelectedProduct { Id = 0, Quantity = 0 });
            }
        }

        public void InitData(ProductService productService)
        {
            OrderItems = new List<OrderItem>();

            ProductOptions = new List<SelectListItem>();
            var products = productService.GetAll().ToList();
            products.ForEach(p => ProductOptions.Add(new SelectListItem(p.Name, p.Id.ToString())));
        }

        public OrderVM(List<OrderItem> orderItems)
        {
            var orderItem = orderItems.FirstOrDefault();

            Id = orderItem.Order.Id;
            CustomerName = orderItem.Order.CustomerName;
            CustomerAddress = orderItem.Order.CustomerAddress;
            Created = orderItem.Order.Created;

            OrderItems = new List<OrderItem>();
            OrderItems = orderItems;
        }
    }

    public class SelectedProduct
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
    }
}
