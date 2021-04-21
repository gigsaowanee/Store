using System.Collections.Generic;
using Store.Models.Store;

namespace Store.DTOs.Store
{
    public class ProductGroupDTO_ToReturn_Product
    {
        public int Id { get; set; }
        public string GroupCode { get; set; }
        public string Name { get; set; }
        public List<ProductDTO_ToReturn> Product { get; set; }
    }
}