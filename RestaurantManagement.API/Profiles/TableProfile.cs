using AutoMapper;
using RestaurantManagement.API.DTOs.Requests;
using RestaurantManagement.API.DTOs.Responses;
using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.API.Profiles;

public class TableProfile : Profile
{
    public TableProfile()
    {
        CreateMap<CreateTableRequest, Table>();

        CreateMap<UpdateTableRequest, Table>();

        CreateMap<Table, TableResponse>()
            .ForMember(
                dest => dest.Status,
                opt => opt.MapFrom(src => src.Status.ToString()));
    }
}