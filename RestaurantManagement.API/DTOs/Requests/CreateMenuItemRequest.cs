namespace RestaurantManagement.API.DTOs.Requests;

public class CreateMenuItemRequest
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Category { get; set; }
    public int RestaurantId { get; set; }
}