using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderDemoCoreMVC.Models.DbSets
{
    public class OrderItem
    {
        //PK
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //Relations
        public Order Order { get; set; }
        public Product Product { get; set; }


        //Attr

        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        //copied from Product to widstand change on product
        public float Price { get; set; }
    }
}
