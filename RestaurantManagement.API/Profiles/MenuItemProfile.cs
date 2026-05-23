using AutoMapper;
using RestaurantManagement.API.DTOs.Requests;
using RestaurantManagement.API.DTOs.Responses;
using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.API.Profiles;

public class MenuItemProfile : Profile
{
    public MenuItemProfile()
    {
        CreateMap<CreateMenuItemRequest, MenuItem>();

        CreateMap<UpdateMenuItemRequest, MenuItem>();

        CreateMap<MenuItem, MenuItemResponse>()
            .ForMember(
                dest => dest.Category,
                opt => opt.MapFrom(src => src.Category.ToString()));
    }
}