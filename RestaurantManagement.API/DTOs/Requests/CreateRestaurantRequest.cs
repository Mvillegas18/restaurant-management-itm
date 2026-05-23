namespace RestaurantManagement.API.DTOs.Requests;

public class CreateRestaurantRequest
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int Capacity { get; set; }
}