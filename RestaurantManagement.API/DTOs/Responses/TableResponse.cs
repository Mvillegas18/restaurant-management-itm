namespace RestaurantManagement.API.DTOs.Responses;

public class TableResponse
{
    public int Id { get; set; }
    public int Number { get; set; }
    public int Capacity { get; set; }
    public string Status { get; set; } = string.Empty;
    public int RestaurantId { get; set; }
}