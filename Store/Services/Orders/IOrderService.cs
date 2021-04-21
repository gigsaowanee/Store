using System.Collections.Generic;
using System.Threading.Tasks;
using Store.Models;
using Store.DTOs.Store;

namespace Store.Services.Orders
{
    public interface IOrderService
    {
         Task<ServiceResponse<OrderDTO_ToReturn>> GetOrderById(int id);

         Task<ServiceResponse<OrderDTO_ToReturn>> InsertOrder(OrderOrderDetailDTO_ToCreate input);
    }
}