using AutoMapper;
using RestaurantManagement.API.DTOs.Requests;
using RestaurantManagement.API.DTOs.Responses;
using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.API.Profiles;

public class RestaurantProfile : Profile
{
    public RestaurantProfile()
    {
        CreateMap<CreateRestaurantRequest, Restaurant>();

        CreateMap<UpdateRestaurantRequest, Restaurant>();

        CreateMap<Restaurant, RestaurantResponse>();
    }
}