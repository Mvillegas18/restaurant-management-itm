namespace RestaurantManagement.API.DTOs.Requests;

public class UpdateMenuItemRequest
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Category { get; set; }
    public bool IsAvailable { get; set; }
}