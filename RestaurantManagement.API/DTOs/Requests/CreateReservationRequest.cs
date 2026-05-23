namespace RestaurantManagement.API.DTOs.Requests;

public class CreateReservationRequest
{
    public DateTime ReservationDate { get; set; }
    public int PartySize { get; set; }
    public string? Notes { get; set; }

    public int CustomerId { get; set; }
    public int TableId { get; set; }
}