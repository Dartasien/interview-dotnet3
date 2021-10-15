using AutoMapper;
using GroceryStoreAPI.Data.Entities;
using GroceryStoreAPI.Services.Models;

namespace GroceryStoreAPI.Services.MappingProfiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerResponse>();
            CreateMap<CustomerRequest, Customer>();
        }
    }
}