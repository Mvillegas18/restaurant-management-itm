using Microsoft.EntityFrameworkCore;
using RestaurantManagement.DataAccess.Context;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Enums;
using RestaurantManagement.Domain.Interfaces;

namespace RestaurantManagement.DataAccess.Repositories;

public class TableRepository : GenericRepository<Table>, ITableRepository
{
    public TableRepository(RestaurantManagementDbContext context)
        : base(context)
    {
    }

    public async Task<IEnumerable<Table>> GetByRestaurantAsync(int restaurantId)
    {
        return await _dbSet
            .Where(table => table.RestaurantId == restaurantId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Table>> GetAvailableTablesAsync(int restaurantId, DateTime date, int partySize)
    {
        var startDate = date.Date;
        var endDate = startDate.AddDays(1);

        return await _dbSet
            .Include(table => table.Reservations)
            .Where(table => table.RestaurantId == restaurantId)
            .Where(table => table.Capacity >= partySize)
            .Where(table => table.Status == TableStatus.Available)
            .Where(table => !table.Reservations.Any(reservation => reservation.ReservationDate >= startDate && reservation.ReservationDate < endDate))
            .ToListAsync();
    }
}
