using AutoMapper;
using RestaurantManagement.API.DTOs.Requests;
using RestaurantManagement.API.DTOs.Responses;
using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.API.Profiles;

public class ReservationProfile : Profile
{
    public ReservationProfile()
    {
        CreateMap<CreateReservationRequest, Reservation>();

        CreateMap<UpdateReservationRequest, Reservation>();

        CreateMap<Reservation, ReservationResponse>()
            .ForMember(
                dest => dest.Status,
                opt => opt.MapFrom(src => src.Status.ToString()));
    }
}