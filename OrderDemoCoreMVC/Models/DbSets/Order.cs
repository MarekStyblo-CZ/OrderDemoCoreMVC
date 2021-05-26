using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderDemoCoreMVC.Models.DbSets
{
    public class Order
    {
        //PK
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //Relations
        public List<OrderItem> OderItem { get; set; }

        //Attr
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public DateTime Created { get; set; }
    }
}
