using Store.DTOs;

namespace Store
{
    public class ProductGroupDTO_Filter :PaginationDto
    {
        public string Name { get; set; }
        public string GroupCode { get; set; }
    }
}