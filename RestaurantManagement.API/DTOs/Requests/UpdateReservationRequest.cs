namespace RestaurantManagement.API.DTOs.Requests;

public class UpdateReservationRequest
{
    public DateTime ReservationDate { get; set; }
    public int PartySize { get; set; }
    public string? Notes { get; set; }
}