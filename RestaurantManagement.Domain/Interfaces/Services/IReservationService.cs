using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Domain.Interfaces;

public interface IReservationService
{
    Task<IEnumerable<Reservation>> GetAllAsync();
    Task<Reservation?> GetByIdAsync(int id);
    Task<Reservation> CreateAsync(Reservation reservation);
    Task<Reservation> UpdateStatusAsync(int id, string status);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<Reservation>> GetByDateAsync(DateTime date);
}