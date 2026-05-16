using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Domain.Interfaces;

public interface IReservationRepository : IGenericRepository<Reservation>
{
    Task<IEnumerable<Reservation>> GetByCustomerAsync(int customerId);
    Task<IEnumerable<Reservation>> GetByDateAsync(DateTime date);
    Task<Reservation?> GetWithDetailsAsync(int id);
}