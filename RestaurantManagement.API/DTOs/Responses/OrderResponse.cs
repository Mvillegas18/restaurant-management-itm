namespace RestaurantManagement.API.DTOs.Responses;

public class OrderResponse
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public decimal TotalAmount { get; set; }
    public string? SpecialInstructions { get; set; }

    public int ReservationId { get; set; }
}