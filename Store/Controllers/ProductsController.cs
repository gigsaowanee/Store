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
using Store.Services.Products;

namespace Store.Controllers.StoreController
{

    [ApiController] 
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _product;

        public ProductsController(IProductService product){
            this._product = product;
        }

        [HttpGet("GetProduct")]
        public async Task<IActionResult> GetProductAsync(){
           var result= await _product.GetProduct();
            return Ok(result);
        }

        [HttpGet("GetProductById/{id}")]
        public async Task<IActionResult> GetProductByIdAsync(int id){
           
            var result= await _product.GetProductById(id);
            return Ok(result);
        }

        [HttpPost("InsertProduct")]
        public async Task<IActionResult> InsertProductAsync(ProductDTO_ToCreate input){

            var result = await _product.InsertProduct(input);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                throw new Exception(result.Message);
            }
        }

        [HttpPut("EditProduct/{id}")]
        public async Task<IActionResult> EditProductAsync(ProductDTO_ToUpdate input ,int id){
           
            var result= await _product.EditProduct(input,id);
            return Ok(result);
        }
        }

     
        
    }
