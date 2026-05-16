using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Domain.Interfaces;

public interface ITableRepository : IGenericRepository<Table>
{
    Task<IEnumerable<Table>> GetByRestaurantAsync(int restaurantId);
    Task<IEnumerable<Table>> GetAvailableTablesAsync(int restaurantId, DateTime date, int partySize);
}