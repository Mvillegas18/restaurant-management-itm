namespace RestaurantManagement.API.DTOs.Responses;

public class OrderItemResponse
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    public int OrderId { get; set; }
    public int MenuItemId { get; set; }
}