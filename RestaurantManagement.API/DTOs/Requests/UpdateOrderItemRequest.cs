namespace RestaurantManagement.API.DTOs.Requests;

public class UpdateOrderItemRequest
{
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}