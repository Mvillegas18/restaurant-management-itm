namespace RestaurantManagement.API.DTOs.Requests;

public class CreateOrderItemRequest
{
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    public int OrderId { get; set; }
    public int MenuItemId { get; set; }
}