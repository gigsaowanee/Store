using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Models.Store
{
        [Table("ProductGroup", Schema = "Store")]
    public class ProductGroup
    {
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "GroupCode can't be null")]
        [StringLength(5, ErrorMessage = "GroupCode can be 1-5 digits")]
        public string GroupCode { get; set; }
        [Required(ErrorMessage = "Name can't be null")]
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public List<Product> Product { get; set; }
    }
}