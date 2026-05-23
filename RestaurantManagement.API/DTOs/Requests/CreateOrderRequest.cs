namespace RestaurantManagement.API.DTOs.Requests;

public class CreateOrderRequest
{
    public decimal TotalAmount { get; set; }
    public string? SpecialInstructions { get; set; }

    public int ReservationId { get; set; }
}