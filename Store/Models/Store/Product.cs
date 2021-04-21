using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Models.Store
{
        [Table("Product", Schema = "Store")]
    public class Product
    {   
        [Key]
        public int Id { get; set; }
        
        [Required]
        public int ProductGroupId { get; set; }
        public ProductGroup ProductGroup { get; set; }
        
        public List<OrderDetail> OrderDetail { get; set; }

        [Required (ErrorMessage = "Name can't be null")]
        public string Name { get; set; }
       
        [Range(1,int.MaxValue,ErrorMessage = "Number of Product can't be 0")]
        public int NumberOfProduct { get; set; }

        [Range(1,int.MaxValue,ErrorMessage = "Price can't be 0")]
        public double Price { get; set; }
        public DateTime CreateDate { get; set; }
    }
}