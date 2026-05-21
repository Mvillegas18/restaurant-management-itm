using Microsoft.EntityFrameworkCore;
using RestaurantManagement.DataAccess.Context;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Interfaces;

namespace RestaurantManagement.DataAccess.Repositories;

public class ReservationRepository : GenericRepository<Reservation>, IReservationRepository
{
    public ReservationRepository(RestaurantManagementDbContext context)
        : base(context)
    {
    }

    public async Task<IEnumerable<Reservation>> GetByCustomerAsync(int customerId)
    {
        return await _dbSet
            .Where(reservation => reservation.CustomerId == customerId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Reservation>> GetByDateAsync(DateTime date)
    {
        var startDate = date.Date;
        var endDate = startDate.AddDays(1);

        return await _dbSet
            .Where(reservation => reservation.ReservationDate >= startDate && reservation.ReservationDate < endDate)
            .ToListAsync();
    }

    public async Task<Reservation?> GetWithDetailsAsync(int id)
    {
        return await _dbSet
            .Include(reservation => reservation.Customer)
            .Include(reservation => reservation.Table)
            .Include(reservation => reservation.Order)
            .ThenInclude(order => order!.OrderItems)
            .ThenInclude(orderItem => orderItem.MenuItem)
            .FirstOrDefaultAsync(reservation => reservation.Id == id);
    }
}
