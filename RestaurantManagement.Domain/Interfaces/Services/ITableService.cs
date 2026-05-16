using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Domain.Interfaces.Services;
public interface ITableService
{
    Task<IEnumerable<Table>> GetAllAsync(int restaurantId);
    Task<Table?> GetByIdAsync(int id);
    Task<Table> CreateAsync(Table table);
    Task<Table> UpdateAsync(Table table);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<Table>> GetAvailableAsync(int restaurantId, DateTime date, int partySize);
}
