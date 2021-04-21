using AutoMapper;
using Store.DTOs;
using Store.DTOs.Store;
using Store.Models;
using Store.Models.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<Role, RoleDto>()
                .ForMember(x => x.RoleName, x => x.MapFrom(x => x.Name));
            CreateMap<RoleDtoAdd, Role>()
                .ForMember(x => x.Name, x => x.MapFrom(x => x.RoleName)); ;
            CreateMap<UserRole, UserRoleDto>();

            CreateMap<ProductGroup, ProductGroupDTO_ToReturn>().ReverseMap();
            CreateMap<Product,ProductDTO_ToReturn>().ReverseMap();
            
            CreateMap<ProductGroup, ProductGroupDTO_ToReturn_Product>().ReverseMap();
            CreateMap<Order, OrderDTO_ToReturn>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailDTO_ToReturn>().ReverseMap();
        }
    }
}