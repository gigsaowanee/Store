using System;
using System.ComponentModel.DataAnnotations;

namespace Store.DTOs.Store
{
    public class ProductGroupDTO_ToCreate
    {
         
        [Required(ErrorMessage = "GroupCode can't be null")]
        [StringLength(5, ErrorMessage = "GroupCode can be 1-5 digits")]
        public string GroupCode { get; set; }
        
        [Required(ErrorMessage = "Name can't be null")]
        public string Name { get; set; }
    }
}