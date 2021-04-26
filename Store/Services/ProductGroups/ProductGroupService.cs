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
using System.Linq.Dynamic.Core;
using Store.Helpers;

namespace Store.Services.ProductGroups
{
    public class ProductGroupService :ServiceBase, IProductGroupService
    {

          private readonly IMapper _mapper;
        private readonly AppDBContext _dbContext;
        private readonly ILogger<ProductGroupService> _log;
        private readonly IHttpContextAccessor _httpContext;

        public ProductGroupService(IMapper mapper, AppDBContext dbContext, ILogger<ProductGroupService> log, IHttpContextAccessor httpContext)
        : base(dbContext, mapper, httpContext)
        {
            this._mapper = mapper;
            this._dbContext = dbContext;
            this._log = log;
            this._httpContext = httpContext;
        }

        public async Task<ServiceResponse<ProductGroupDTO_ToReturn>> EditProductGroup(ProductGroupDTO_ToUpdate input, int id)
        {
            var productGroup = await _dbContext.ProductGroups.Where(x => x.Id == id).FirstOrDefaultAsync();

            if(productGroup == null)
            {
                return ResponseResult.Failure<ProductGroupDTO_ToReturn>("Not value id: "+ id);
            }
            else
            {
                productGroup.Name = input.Name;
                productGroup.GroupCode = input.GroupCode;
                await _dbContext.SaveChangesAsync();
                
                var result = _mapper.Map<ProductGroupDTO_ToReturn>(productGroup);
                return ResponseResult.Success(result);
           }
        }

        public async Task<ServiceResponse<List<ProductGroupDTO_ToReturn_Product>>> GetProductByProductGroupId(int id)
        {
            var product = await _dbContext.ProductGroups.Include(x => x.Product).Where(x => x.Id == id).ToListAsync();
            var result = _mapper.Map<List<ProductGroupDTO_ToReturn_Product>>(product);

            return ResponseResult.Success(result);

        }

        public async Task<ServiceResponse<List<ProductGroupDTO_ToReturn>>> GetProductGroup()
        {
             var productGroup = await _dbContext.ProductGroups.ToListAsync();
            var result = _mapper.Map<List<ProductGroupDTO_ToReturn>>(productGroup);
            return ResponseResult.Success(result);
        }

        public async Task<ServiceResponse<ProductGroupDTO_ToReturn>> GetProductGroupById(int id)
        {
            var productGroup = await _dbContext.ProductGroups.Where(x => x.Id == id).FirstOrDefaultAsync();
            var result = _mapper.Map<ProductGroupDTO_ToReturn>(productGroup);

            return ResponseResult.Success(result);
        }

        public async Task<ServiceResponse<ProductGroupDTO_ToReturn>> InsertProductGroup(ProductGroupDTO_ToCreate input)
        {
            try
            {
                 var productGroup = new ProductGroup();

            productGroup.Name = input.Name;
            productGroup.GroupCode = input.GroupCode;
            productGroup.CreateDate = DateTime.Now;
            await _dbContext.ProductGroups.AddAsync(productGroup);
            await _dbContext.SaveChangesAsync();

            var result = _mapper.Map<ProductGroupDTO_ToReturn>(productGroup);

            return ResponseResult.Success(result);
            }
            catch (System.Exception ex)
            {
                
                return ResponseResult.Failure<ProductGroupDTO_ToReturn>(ex.Message);
            }
       }

        public async Task<ServiceResponse<List<ProductGroupDTO_ToReturn>>> SearchProductGroupPaginate(ProductGroupDTO_Filter filter)
        {
             var queryable = _dbContext.ProductGroups.Include(x => x.Product).AsQueryable();
            //filter
             if(!String.IsNullOrWhiteSpace(filter.Name))
             {
                 queryable = queryable.Where(x => (x.Name).Contains(filter.Name));
             }

              if(!String.IsNullOrWhiteSpace(filter.GroupCode))
             {
                 
                 queryable = queryable.Where(x => (x.GroupCode).Contains(filter.GroupCode));
             }

            //Order By
              if (!string.IsNullOrWhiteSpace(filter.OrderingField))
            {
                try
                {
                    queryable = queryable.OrderBy($"{filter.OrderingField} {(filter.AscendingOrder ? "ascending" : "descending")}");
                }
                catch
                {
                    return ResponseResultWithPagination.Failure<List<ProductGroupDTO_ToReturn>>($"Could not order by field: {filter.OrderingField}");
                }
            }


                //Add Paginate
                 var paginationResult = await _httpContext.HttpContext.InsertPaginationParametersInResponse(queryable, filter.RecordsPerPage, filter.Page);

                //Excute query
                var productGroup = await queryable.Paginate(filter).ToListAsync();

                var result = _mapper.Map<List<ProductGroupDTO_ToReturn>>(productGroup);

                return ResponseResultWithPagination.Success(result, paginationResult);
        }
    }
}