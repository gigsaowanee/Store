using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Data;
using Store.DTOs.Store;
using Store.Models.Store;
using Store.Services.Orders;

namespace Store.Controllers.StoreController
{

     [ApiController] 
    [Route("api/[controller]")]
    [Authorize]
    public class OrdersController: ControllerBase
    {
        private readonly IOrderService _order;

        public OrdersController(IOrderService order){
            this._order = order;
        }
        
        [HttpPost("InsertOrder")]
        public async Task<IActionResult> InsertOrder(OrderOrderDetailDTO_ToCreate input){
            var result = await _order.InsertOrder(input);

            if(result.IsSuccess)
            {
            return Ok(result);  
            }
            else
            {
                throw new Exception(result.Message);
            } 

        }

        [HttpGet("GetorderById/{id}")]
        [Authorize(Roles= "Manager")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            Console.WriteLine("dev1");
         
           var result = await _order.GetOrderById(id);

            return Ok(result);
       }

        [HttpGet("SearchOrderPaginate")]
        public async Task<IActionResult> SearchOrderPaginate([FromQuery] OrderDTO_Filter filter)
        {
            Console.WriteLine("dev1");

            var result = await _order.SearchOrderPaginate(filter);
            return Ok(result);
        }



        }

    }
