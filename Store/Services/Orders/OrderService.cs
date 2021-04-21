using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Store.Data;
using Store.Models;
using Store.Services;
using Store.DTOs.Store;
using Store.Models.Store;

namespace Store.Services.Orders
{
    public class OrderService:ServiceBase,IOrderService
    {
        private readonly IMapper _mapper;
        private readonly AppDBContext _dbContext;
        private readonly ILogger<OrderService> _log;
        private readonly IHttpContextAccessor _httpContext;

        public OrderService(IMapper mapper, AppDBContext dbContext, ILogger<OrderService> log ,IHttpContextAccessor httpContext )
        : base(dbContext, mapper, httpContext){
            this._mapper = mapper;
            this._dbContext = dbContext;
            this._log = log;
            this._httpContext = httpContext;
        }

        public async Task<ServiceResponse<OrderDTO_ToReturn>> GetOrderById(int id)
        {
            var order = await _dbContext.Orders.Include(x => x.OrderDetail).ThenInclude(x => x.Product).ThenInclude(x => x.ProductGroup).Where(x => x.Id == id).FirstOrDefaultAsync();

            if(order == null){
                return ResponseResult.Failure<OrderDTO_ToReturn>("Not Found id : " + id);
            }
            else
            {
            var result = _mapper.Map<OrderDTO_ToReturn>(order);
            return ResponseResult.Success(result);
             }
          }

        public async Task<ServiceResponse<OrderDTO_ToReturn>> InsertOrder(OrderOrderDetailDTO_ToCreate input)
        {   
            double totalPrice = 0;
            int totalCount = 0;

            foreach(var i in input.OrderDetail)
            {
            var product = await _dbContext.Products.Where(x => x.Id == i.ProductId).FirstOrDefaultAsync();
             totalPrice += product.Price*i.QTY;
             totalCount += i.QTY;
            }

            try
            {
                var order =  new Order();

            order.TotalPrice = totalPrice;
            order.TotalCount = totalCount;
            order.PaymentType = input.Order.PaymentType;
            order.OrderStatus = input.Order.OrderStatus;
            order.OrderDate = DateTime.Now;

           await _dbContext.Orders.AddAsync(order);
           await _dbContext.SaveChangesAsync();

            foreach(var i in input.OrderDetail){

            var checkOrderDetail = await _dbContext.OrderDetails.Where( x => x.ProductId == i.ProductId && x.OrderId == order.Id).FirstOrDefaultAsync();

            if (checkOrderDetail == null)
            {
                 var orderDetail = new OrderDetail();
              orderDetail.OrderId = order.Id;
              orderDetail.ProductId = i.ProductId;
              orderDetail.QTY = i.QTY;
              orderDetail.OrderDetailDate = DateTime.Now;

              await _dbContext.OrderDetails.AddAsync(orderDetail);
              await _dbContext.SaveChangesAsync();
            }else{

                checkOrderDetail.QTY = checkOrderDetail.QTY + i.QTY;
              await  _dbContext.SaveChangesAsync();
            }
          }

        var returnOrder = await _dbContext.Orders.Include(x => x.OrderDetail).ThenInclude(x => x.Product).Where(x => x.Id == order.Id).FirstOrDefaultAsync();
        var result = _mapper.Map<OrderDTO_ToReturn>(returnOrder);

        return ResponseResult.Success(result);
                
            }
            catch (System.Exception ex)
            {
                
               return ResponseResult.Failure<OrderDTO_ToReturn>(ex.Message);
            }
             
        }
    }
}