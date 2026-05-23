namespace RestaurantManagement.API.DTOs.Requests;

public class CreateTableRequest
{
    public int Number { get; set; }
    public int Capacity { get; set; }
    public int RestaurantId { get; set; }
}