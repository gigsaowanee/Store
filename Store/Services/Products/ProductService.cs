using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Store.Data;
using Store.DTOs.Store;
using Store.Models;
using Store.Models.Store;

namespace Store.Services.Products
{
    public class ProductService : ServiceBase, IProductService
    {

        private readonly IMapper _mapper;
        private readonly AppDBContext _dbContext;
        private readonly ILogger<ProductService> _log;
        private readonly IHttpContextAccessor _httpContext;

        public ProductService(IMapper mapper, AppDBContext dbContext, ILogger<ProductService> log, IHttpContextAccessor httpContext)
        : base(dbContext, mapper, httpContext)
        {
            this._mapper = mapper;
            this._dbContext = dbContext;
            this._log = log;
            this._httpContext = httpContext;
        }


        public async Task<ServiceResponse<ProductDTO_ToReturn>> EditProduct(ProductDTO_ToUpdate input, int id)
        {
            var product = await _dbContext.Products.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (product == null)
            {
                return ResponseResult.Failure<ProductDTO_ToReturn>("Not found value of id " + id);
            }
            else
            {
                product.Name = input.Name;
                product.Price = input.Price;
                product.ProductGroupId = input.ProductGroupId;
                product.NumberOfProduct = input.NumberOfProduct;
                product.CreateDate = DateTime.Now;

                await _dbContext.SaveChangesAsync();

                var result = _mapper.Map<ProductDTO_ToReturn>(product);
                return ResponseResult.Success(result);

            }
        }

            public async Task<ServiceResponse<List<ProductDTO_ToReturn>>> GetProduct()
            {
                var product = await _dbContext.Products.Include(x => x.ProductGroup).ToListAsync();
                var result = _mapper.Map<List<ProductDTO_ToReturn>>(product);

                return ResponseResult.Success(result);
            }

        public async Task<ServiceResponse<ProductDTO_ToReturn>> GetProductById(int id)
        {
            var product = await _dbContext.Products.Include(x => x.ProductGroup).Where(x => x.Id == id).FirstOrDefaultAsync();

            if(product == null)
            {
                return ResponseResult.Failure<ProductDTO_ToReturn>("Not found Id");
            }
            else
            {
            var result = _mapper.Map<ProductDTO_ToReturn>(product);
            return ResponseResult.Success(result);
            }
            
        }

        public async Task<ServiceResponse<ProductDTO_ToReturn>> InsertProduct(ProductDTO_ToCreate input)
        {
            try
            {
              var product = new Product();

            product.Name = input.Name;
            product.Price = input.Price;
            product.ProductGroupId = input.ProductGroupId;
            product.NumberOfProduct = input.NumberOfProduct;
            product.CreateDate = DateTime.Now;

            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            var result = _mapper.Map<ProductDTO_ToReturn>(product);
            return ResponseResult.Success(result);
            }
            catch (System.Exception ex)
            {
                
               return ResponseResult.Failure<ProductDTO_ToReturn>(ex.Message);
            }
             
        }
    }
}