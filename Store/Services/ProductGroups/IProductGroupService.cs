using System.Collections.Generic;
using System.Threading.Tasks;
using Store.DTOs.Store;
using Store.Models;

namespace Store.Services.ProductGroups
{
    public interface IProductGroupService
    {
         Task<ServiceResponse<List<ProductGroupDTO_ToReturn>>> GetProductGroup();
         Task<ServiceResponse<ProductGroupDTO_ToReturn>> GetProductGroupById(int id);
         Task<ServiceResponse<ProductGroupDTO_ToReturn>> InsertProductGroup(ProductGroupDTO_ToCreate input);
         Task <ServiceResponse<List<ProductGroupDTO_ToReturn_Product>>> GetProductByProductGroupId(int id);
         Task<ServiceResponse<ProductGroupDTO_ToReturn>> EditProductGroup(ProductGroupDTO_ToUpdate input, int id);

    }
}