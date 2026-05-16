using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Domain.Interfaces;

public interface IMenuItemService
{
    Task<IEnumerable<MenuItem>> GetAllAsync(int restaurantId);
    Task<MenuItem?> GetByIdAsync(int id);
    Task<MenuItem> CreateAsync(MenuItem item);
    Task<MenuItem> UpdateAsync(MenuItem item);
    Task<bool> DeleteAsync(int id);
}