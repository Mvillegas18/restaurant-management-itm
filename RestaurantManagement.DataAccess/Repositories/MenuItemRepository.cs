using Microsoft.EntityFrameworkCore;
using RestaurantManagement.DataAccess.Context;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Interfaces;

namespace RestaurantManagement.DataAccess.Repositories;

public class MenuItemRepository : GenericRepository<MenuItem>, IMenuItemRepository
{
    public MenuItemRepository(RestaurantManagementDbContext context)
        : base(context)
    {
    }

    public async Task<IEnumerable<MenuItem>> GetByRestaurantAsync(int restaurantId)
    {
        return await _dbSet
            .Where(menuItem => menuItem.RestaurantId == restaurantId)
            .ToListAsync();
    }

    public async Task<IEnumerable<MenuItem>> GetByCategoryAsync(int restaurantId, string category)
    {
        return await _dbSet
            .Where(menuItem => menuItem.RestaurantId == restaurantId && menuItem.Category.ToString() == category)
            .ToListAsync();
    }
}
