namespace RestaurantManagement.API.DTOs.Responses;

public class ReservationResponse
{
    public int Id { get; set; }
    public DateTime ReservationDate { get; set; }
    public int PartySize { get; set; }
    public string? Notes { get; set; }
    public string Status { get; set; } = string.Empty;

    public int CustomerId { get; set; }
    public int TableId { get; set; }
}