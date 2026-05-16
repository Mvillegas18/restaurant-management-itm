namespace RestaurantManagement.Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public decimal TotalAmount { get; set; }
    public string? SpecialInstructions { get; set; }

    // FK 1:1 con Reservation
    public int ReservationId { get; set; }
    public Reservation Reservation { get; set; } = null!;

    // Navigation N:M hacia MenuItem via OrderItem
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}