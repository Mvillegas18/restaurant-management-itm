using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Enums;
using RestaurantManagement.Domain.Interfaces;

namespace RestaurantManagement.Domain.Services;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepo;
    private readonly ITableRepository _tableRepo;

    public ReservationService(IReservationRepository reservationRepo, ITableRepository tableRepo)
    {
        _reservationRepo = reservationRepo;
        _tableRepo = tableRepo;
    }

    public async Task<IEnumerable<Reservation>> GetAllAsync()
        => await _reservationRepo.GetAllAsync();

    public async Task<Reservation?> GetByIdAsync(int id)
        => await _reservationRepo.GetWithDetailsAsync(id);

    public async Task<Reservation> CreateAsync(Reservation reservation)
    {
        // Validación: fecha futura
        if (reservation.ReservationDate <= DateTime.UtcNow)
            throw new InvalidOperationException("La fecha de reserva debe ser en el futuro.");

        // Validación: tamaño del grupo
        if (reservation.PartySize <= 0)
            throw new InvalidOperationException("El tamaño del grupo debe ser mayor a 0.");

        // Validación: mesa disponible
        var table = await _tableRepo.GetByIdAsync(reservation.TableId)
            ?? throw new InvalidOperationException("La mesa no existe.");

        if (table.Status != TableStatus.Available)
            throw new InvalidOperationException("La mesa no está disponible.");

        if (table.Capacity < reservation.PartySize)
            throw new InvalidOperationException(
                $"La mesa solo tiene capacidad para {table.Capacity} personas.");

        reservation.Status = ReservationStatus.Pending;
        var created = await _reservationRepo.CreateAsync(reservation);

        // Marcar mesa como reservada
        table.Status = TableStatus.Reserved;
        await _tableRepo.UpdateAsync(table);

        return created;
    }

    public async Task<Reservation> UpdateStatusAsync(int id, string status)
    {
        var reservation = await _reservationRepo.GetByIdAsync(id)
            ?? throw new InvalidOperationException("La reserva no existe.");

        if (!Enum.TryParse<ReservationStatus>(status, true, out var newStatus))
            throw new InvalidOperationException($"Estado '{status}' no válido.");

        reservation.Status = newStatus;

        // Liberar mesa si se cancela o completa
        if (newStatus is ReservationStatus.Cancelled or ReservationStatus.Completed)
        {
            var table = await _tableRepo.GetByIdAsync(reservation.TableId);
            if (table != null)
            {
                table.Status = TableStatus.Available;
                await _tableRepo.UpdateAsync(table);
            }
        }

        return await _reservationRepo.UpdateAsync(reservation);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        if (!await _reservationRepo.ExistsAsync(id))
            throw new InvalidOperationException("La reserva no existe.");
        return await _reservationRepo.DeleteAsync(id);
    }

    public async Task<IEnumerable<Reservation>> GetByDateAsync(DateTime date)
        => await _reservationRepo.GetByDateAsync(date);
}