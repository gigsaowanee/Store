using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Models.Store
{
    [Table("Order", Schema = "Store")]
    public class Order
    {
        [Key]
        public int Id { get; set; }  
        public double TotalPrice { get; set; }
        public int TotalCount { get; set; }
        public string PaymentType { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }  
        public List<OrderDetail> OrderDetail { get; set; }
       

    }
}