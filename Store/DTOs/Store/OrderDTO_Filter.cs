using Store.DTOs;

namespace Store
{
    public class OrderDTO_Filter : PaginationDto
    {
         public string PaymentType { get; set; }

         public double MinPrice { get; set; } 
    }
}