using System.ComponentModel.DataAnnotations;

namespace Store.DTOs.Store
{
    public class OrderDTO_ToCreate
    {
        [Required(ErrorMessage = "Payment Type can't be null")]
        public string PaymentType { get; set; }

        [Required(ErrorMessage = "Status can't be null")]
        public string OrderStatus { get; set; }  
    }
}