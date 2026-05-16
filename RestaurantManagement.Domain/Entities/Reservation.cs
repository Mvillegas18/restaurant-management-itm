using RestaurantManagement.Domain.Enums;

namespace RestaurantManagement.Domain.Entities;

public class Reservation
{
    public int Id { get; set; }
    public DateTime ReservationDate { get; set; }
    public int PartySize { get; set; }
    public string? Notes { get; set; }
    public ReservationStatus Status { get; set; } = ReservationStatus.Pending;

    // FK
    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;

    public int TableId { get; set; }
    public Table Table { get; set; } = null!;

    // Navigation
    public Order? Order { get; set; }
}