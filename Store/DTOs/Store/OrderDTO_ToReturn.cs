using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Store.Models.Store;

namespace Store.DTOs.Store
{
    public class OrderDTO_ToReturn
    {
        public int Id { get; set; }
        public double TotalPrice { get; set; }
        public int TotalCount { get; set; }
        public string PaymentType { get; set; }
        public string OrderStatus { get; set; }  
        public List<OrderDetailDTO_ToReturn> OrderDetail { get; set; }
    }
}