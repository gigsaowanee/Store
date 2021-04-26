using Store.DTOs;

namespace Store
{
    public class ProductDTO_Filter :PaginationDto
    {
        public string Name { get; set; }
        public int ProductGroupId { get; set; }
    }
}