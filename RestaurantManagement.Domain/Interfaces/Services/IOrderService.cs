using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Domain.Interfaces;

public interface IOrderService
{
    Task<Order?> GetByIdAsync(int id);
    Task<Order> CreateAsync(Order order, List<(int menuItemId, int quantity)> items);
    Task<Order> UpdateAsync(Order order);
    Task<bool> DeleteAsync(int id);
}