using AutoMapper;
using RestaurantManagement.API.DTOs.Requests;
using RestaurantManagement.API.DTOs.Responses;
using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.API.Profiles;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<CreateCustomerRequest, Customer>();

        CreateMap<UpdateCustomerRequest, Customer>();

        CreateMap<Customer, CustomerResponse>();
    }
}