

namespace Store.DTOs.Store
{
    public class ProductDTO_ToReturn
    {
       
        public int Id { get; set; }
        public int ProductGroupId { get; set; }
        public string Name { get; set; }
        public int NumberOfProduct { get; set; }
        public int Price { get; set; }
        public ProductGroupDTO_ToReturn ProductGroup { get; set; }
    }
}