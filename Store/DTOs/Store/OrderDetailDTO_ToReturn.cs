namespace Store.DTOs.Store
{
    public class OrderDetailDTO_ToReturn
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int QTY { get; set; }
        public ProductDTO_ToReturn Product { get; set; }
    }
}