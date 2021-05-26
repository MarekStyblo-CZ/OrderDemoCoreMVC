using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OrderDemoCoreMVC.ViewModels
{
    public class ProductVM
    {
        [DisplayName("Id")]
        public int Id { get; set; }

        [DisplayName("Kód")]
        [Range(1, int.MaxValue, ErrorMessage = "Je třeba zada číslo větší než nula {1}")]
        [Required]
        public int Code { get; set; }

        [DisplayName("Název")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Je třeba zadat název")]
        public string Name { get; set; }

        [DisplayName("Cena [CZK]")]
        [Range(1, int.MaxValue, ErrorMessage = "Je třeba zada číslo větší než nula {1}")]
        [Required]
        public float Price { get; set; }
    }
}
