using System.ComponentModel.DataAnnotations;
using Store.Models.Store;

namespace Store.DTOs.Store
{
    public class ProductDTO_ToCreate
    {
        public int ProductGroupId { get; set; }
        
        [Required (ErrorMessage = "Name can't be null")]
        public string Name { get; set; }
       
        [Range(1,int.MaxValue,ErrorMessage = "Number of Product can't be 0")]
        public int NumberOfProduct { get; set; }
        
        [Range(1,int.MaxValue,ErrorMessage = "Price can't be 0")]
        public int Price { get; set; }
        
    }
}