using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Interfaces;

namespace RestaurantManagement.Domain.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepo;
    private readonly IMenuItemRepository _menuItemRepo;
    private readonly IReservationRepository _reservationRepo;

    public OrderService(IOrderRepository orderRepo, IMenuItemRepository menuItemRepo,
        IReservationRepository reservationRepo)
    {
        _orderRepo = orderRepo;
        _menuItemRepo = menuItemRepo;
        _reservationRepo = reservationRepo;
    }

    public async Task<Order?> GetByIdAsync(int id)
        => await _orderRepo.GetWithItemsAsync(id);

    public async Task<Order> CreateAsync(Order order, List<(int menuItemId, int quantity)> items)
    {
        if (!items.Any())
            throw new InvalidOperationException("La orden debe tener al menos un ítem.");

        var reservation = await _reservationRepo.GetByIdAsync(order.ReservationId)
            ?? throw new InvalidOperationException("La reserva no existe.");

        // Verificar que no tenga ya una orden
        var existing = await _orderRepo.GetByReservationAsync(order.ReservationId);
        if (existing != null)
            throw new InvalidOperationException("Esta reserva ya tiene una orden asociada.");

        order.CreatedAt = DateTime.UtcNow;
        order.OrderItems = new List<OrderItem>();
        decimal total = 0;

        foreach (var (menuItemId, quantity) in items)
        {
            var menuItem = await _menuItemRepo.GetByIdAsync(menuItemId)
                ?? throw new InvalidOperationException($"El ítem de menú {menuItemId} no existe.");

            if (!menuItem.IsAvailable)
                throw new InvalidOperationException($"'{menuItem.Name}' no está disponible actualmente.");

            if (quantity <= 0)
                throw new InvalidOperationException("La cantidad debe ser mayor a 0.");

            order.OrderItems.Add(new OrderItem
            {
                MenuItemId = menuItemId,
                Quantity = quantity,
                UnitPrice = menuItem.Price
            });

            total += menuItem.Price * quantity;
        }

        order.TotalAmount = total;
        return await _orderRepo.CreateAsync(order);
    }

    public async Task<Order> UpdateAsync(Order order)
    {
        if (!await _orderRepo.ExistsAsync(order.Id))
            throw new InvalidOperationException("La orden no existe.");
        return await _orderRepo.UpdateAsync(order);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        if (!await _orderRepo.ExistsAsync(id))
            throw new InvalidOperationException("La orden no existe.");
        return await _orderRepo.DeleteAsync(id);
    }
}