using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Data;
using Store.DTOs.Store;
using Store.Models.Store;
using Store.Services.ProductGroups;

namespace Store.Controllers.StoreController
{
    [ApiController] 
    [Route("api/[controller]")]
    public class ProductGroupsController : ControllerBase
    {
        private readonly IProductGroupService _productGroup;

        public ProductGroupsController(IProductGroupService productGroup){
            this._productGroup = productGroup;
        }

        [HttpGet("GetProductGroup")]
        public async Task<IActionResult> GetProductGroup(){
           
            var result = await _productGroup.GetProductGroup();
            return Ok(result);
        }

         [HttpGet("GetProductGroupById/{id}")]
        public async Task<IActionResult> GetProductGroupById(int id){
            
             var result = await _productGroup.GetProductByProductGroupId(id);
            return Ok(result);
        }

        [HttpPost("InsertProductGroup")]
        public async Task<IActionResult> InsertProductGroup(ProductGroupDTO_ToCreate input){
            

            var result = await _productGroup.InsertProductGroup(input);

            if(result.IsSuccess)
            {
            return Ok(result);  
            }
            else
            {
                throw new Exception(result.Message);
            } 
        }

        [HttpPut("EditProductGroup/{id}")]
        public async Task<IActionResult> EditProductGroup(ProductGroupDTO_ToUpdate input ,int id){
            
                var result = await _productGroup.EditProductGroup(input,id);
                return Ok(result);
            }
        

        [HttpGet("GetProductByProductGroupId")]
        public async Task<IActionResult> GetProductByProductGroupId(int id){
            
            var result = await _productGroup.GetProductByProductGroupId(id);
            return Ok(result);
        }

        
           [HttpGet("SearchProductGroupPaginate")]
        public async Task<IActionResult> SearchProductGroupPaginate([FromQuery] ProductGroupDTO_Filter filter){
           
            var result= await _productGroup.SearchProductGroupPaginate(filter);
            return Ok(result);
        }
    }
}