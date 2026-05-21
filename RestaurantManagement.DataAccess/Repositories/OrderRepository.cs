using Microsoft.EntityFrameworkCore;
using RestaurantManagement.DataAccess.Context;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Interfaces;

namespace RestaurantManagement.DataAccess.Repositories;

public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    public OrderRepository(RestaurantManagementDbContext context)
        : base(context)
    {
    }

    public async Task<Order?> GetWithItemsAsync(int id)
    {
        return await _dbSet
            .Include(order => order.OrderItems)
            .ThenInclude(orderItem => orderItem.MenuItem)
            .FirstOrDefaultAsync(order => order.Id == id);
    }

    public async Task<Order?> GetByReservationAsync(int reservationId)
    {
        return await _dbSet
            .Include(order => order.OrderItems)
            .ThenInclude(orderItem => orderItem.MenuItem)
            .FirstOrDefaultAsync(order => order.ReservationId == reservationId);
    }
}
