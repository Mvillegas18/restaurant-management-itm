namespace RestaurantManagement.API.DTOs.Requests;

public class UpdateOrderRequest
{
    public decimal TotalAmount { get; set; }
    public string? SpecialInstructions { get; set; }
}