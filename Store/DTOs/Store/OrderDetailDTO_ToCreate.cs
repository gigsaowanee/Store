
using System.ComponentModel.DataAnnotations;

namespace Store.DTOs.Store
{
    public class OrderDetailDTO_ToCreate
    {
        [Range(1,int.MaxValue)]
        public int ProductId { get; set; }
        [Range(1,int.MaxValue)]
        public int QTY { get; set; }

    
    }
}