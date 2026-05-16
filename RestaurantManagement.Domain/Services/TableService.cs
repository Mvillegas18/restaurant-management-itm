
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Interfaces;
using RestaurantManagement.Domain.Interfaces.Services;

namespace RestauranteAPI.Domain.Services;
public class TableService : ITableService
{
    private readonly ITableRepository _repo;

    public TableService(ITableRepository repo) => _repo = repo;

    public async Task<IEnumerable<Table>> GetAllAsync(int restaurantId)
        => await _repo.GetByRestaurantAsync(restaurantId);

    public async Task<Table?> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);

    public async Task<Table> CreateAsync(Table table)
    {
        if (table.Number <= 0)
            throw new InvalidOperationException("El número de mesa debe ser mayor a 0.");
        if (table.Capacity <= 0)
            throw new InvalidOperationException("La capacidad debe ser mayor a 0.");
        return await _repo.CreateAsync(table);
    }

    public async Task<Table> UpdateAsync(Table table)
    {
        if (!await _repo.ExistsAsync(table.Id))
            throw new InvalidOperationException("La mesa no existe.");
        return await _repo.UpdateAsync(table);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        if (!await _repo.ExistsAsync(id))
            throw new InvalidOperationException("La mesa no existe.");
        return await _repo.DeleteAsync(id);
    }

    public async Task<IEnumerable<Table>> GetAvailableAsync(int restaurantId, DateTime date, int partySize)
        => await _repo.GetAvailableTablesAsync(restaurantId, date, partySize);
}