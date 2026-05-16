using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Interfaces;

namespace RestaurantManagement.Domain.Services;

public class MenuItemService : IMenuItemService
{
    private readonly IMenuItemRepository _repo;

    public MenuItemService(IMenuItemRepository repo) => _repo = repo;

    public async Task<IEnumerable<MenuItem>> GetAllAsync(int restaurantId)
        => await _repo.GetByRestaurantAsync(restaurantId);

    public async Task<MenuItem?> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);

    public async Task<MenuItem> CreateAsync(MenuItem item)
    {
        if (string.IsNullOrWhiteSpace(item.Name))
            throw new InvalidOperationException("El nombre del ítem no puede estar vacío.");
        if (item.Price <= 0)
            throw new InvalidOperationException("El precio debe ser mayor a 0.");
        return await _repo.CreateAsync(item);
    }

    public async Task<MenuItem> UpdateAsync(MenuItem item)
    {
        if (!await _repo.ExistsAsync(item.Id))
            throw new InvalidOperationException("El ítem de menú no existe.");
        if (item.Price <= 0)
            throw new InvalidOperationException("El precio debe ser mayor a 0.");
        return await _repo.UpdateAsync(item);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        if (!await _repo.ExistsAsync(id))
            throw new InvalidOperationException("El ítem de menú no existe.");
        return await _repo.DeleteAsync(id);
    }
}