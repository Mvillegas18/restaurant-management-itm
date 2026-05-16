using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Domain.Interfaces;

public interface IMenuItemRepository : IGenericRepository<MenuItem>
{
    Task<IEnumerable<MenuItem>> GetByRestaurantAsync(int restaurantId);
    Task<IEnumerable<MenuItem>> GetByCategoryAsync(int restaurantId, string category);
}