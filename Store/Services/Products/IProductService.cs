using System.Collections.Generic;
using System.Threading.Tasks;
using Store.DTOs.Store;
using Store.Models;

namespace Store.Services.Products
{
    public interface IProductService
    {
         Task<ServiceResponse<List<ProductDTO_ToReturn>>> GetProduct();
         Task<ServiceResponse<ProductDTO_ToReturn>> GetProductById(int id);
         Task<ServiceResponse<ProductDTO_ToReturn>> InsertProduct(ProductDTO_ToCreate input);
         Task<ServiceResponse<ProductDTO_ToReturn>> EditProduct(ProductDTO_ToUpdate input, int id);
    }
}