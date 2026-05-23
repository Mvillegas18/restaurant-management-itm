using AutoMapper;
using RestaurantManagement.API.DTOs.Requests;
using RestaurantManagement.API.DTOs.Responses;
using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.API.Profiles;

public class OrderItemProfile : Profile
{
    public OrderItemProfile()
    {
        CreateMap<CreateOrderItemRequest, OrderItem>();

        CreateMap<UpdateOrderItemRequest, OrderItem>();

        CreateMap<OrderItem, OrderItemResponse>();
    }
}