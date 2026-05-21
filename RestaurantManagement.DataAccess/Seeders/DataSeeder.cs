using Microsoft.EntityFrameworkCore;
using RestaurantManagement.DataAccess.Context;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Enums;

namespace RestaurantManagement.DataAccess.Seeders;

public static class DataSeeder
{
    public static async Task SeedAsync(RestaurantManagementDbContext context)
    {
        if (await context.Restaurants.AnyAsync())
        {
            return;
        }

        var restaurants = new List<Restaurant>
        {
            new()
            {
                Name = "Bistró Central",
                Address = "Calle 45 #12-34",
                Phone = "(601) 555-0101",
                Email = "central@bistro.com",
                Capacity = 120
            },
            new()
            {
                Name = "Mar y Tierra",
                Address = "Carrera 7 #85-10",
                Phone = "(601) 555-0115",
                Email = "reservas@marytierra.com",
                Capacity = 90
            },
            new()
            {
                Name = "Sabores del Valle",
                Address = "Av. 6 Norte #21-15",
                Phone = "(602) 555-0120",
                Email = "contacto@saboresvalle.com",
                Capacity = 80
            }
        };

        context.Restaurants.AddRange(restaurants);
        await context.SaveChangesAsync();

        var tables = new List<Table>();
        var tableNumbers = 1;

        foreach (var restaurant in restaurants)
        {
            for (var i = 0; i < 8; i++)
            {
                tables.Add(new Table
                {
                    Number = tableNumbers++,
                    Capacity = i < 4 ? 2 : 4,
                    Status = TableStatus.Available,
                    RestaurantId = restaurant.Id
                });
            }
        }

        context.Tables.AddRange(tables);
        await context.SaveChangesAsync();

        var customers = new List<Customer>
        {
            new() { Name = "Andrea López", Email = "andrea.lopez@email.com", Phone = "3001234567" },
            new() { Name = "Camilo Pérez", Email = "camilo.perez@email.com", Phone = "3012345678" },
            new() { Name = "Valentina Gómez", Email = "valentina.gomez@email.com", Phone = "3023456789" },
            new() { Name = "Santiago Ruiz", Email = "santiago.ruiz@email.com", Phone = "3034567890" },
            new() { Name = "Mariana Torres", Email = "mariana.torres@email.com", Phone = "3045678901" }
        };

        context.Customers.AddRange(customers);
        await context.SaveChangesAsync();

        var menuItems = new List<MenuItem>
        {
            new()
            {
                Name = "Ceviche clásico",
                Description = "Pescado fresco con cítricos y hierbas",
                Price = 32000m,
                Category = MenuCategory.Starter,
                IsAvailable = true,
                RestaurantId = restaurants[0].Id
            },
            new()
            {
                Name = "Lomo en salsa de vino",
                Description = "Lomo sellado con reducción de vino tinto",
                Price = 52000m,
                Category = MenuCategory.MainCourse,
                IsAvailable = true,
                RestaurantId = restaurants[0].Id
            },
            new()
            {
                Name = "Tiramisú",
                Description = "Clásico postre italiano",
                Price = 18000m,
                Category = MenuCategory.Dessert,
                IsAvailable = true,
                RestaurantId = restaurants[0].Id
            },
            new()
            {
                Name = "Arroz marinero",
                Description = "Arroz cremoso con mariscos",
                Price = 48000m,
                Category = MenuCategory.MainCourse,
                IsAvailable = true,
                RestaurantId = restaurants[1].Id
            },
            new()
            {
                Name = "Ensalada tropical",
                Description = "Mix de frutas y vegetales",
                Price = 22000m,
                Category = MenuCategory.Starter,
                IsAvailable = true,
                RestaurantId = restaurants[1].Id
            },
            new()
            {
                Name = "Cheesecake de maracuyá",
                Description = "Postre cremoso con fruta tropical",
                Price = 19000m,
                Category = MenuCategory.Dessert,
                IsAvailable = true,
                RestaurantId = restaurants[1].Id
            },
            new()
            {
                Name = "Sancocho valluno",
                Description = "Sancocho tradicional con pollo",
                Price = 35000m,
                Category = MenuCategory.MainCourse,
                IsAvailable = true,
                RestaurantId = restaurants[2].Id
            },
            new()
            {
                Name = "Empanadas de pipián",
                Description = "Empanadas típicas del Valle",
                Price = 12000m,
                Category = MenuCategory.Starter,
                IsAvailable = true,
                RestaurantId = restaurants[2].Id
            },
            new()
            {
                Name = "Manjar blanco",
                Description = "Postre tradicional vallecaucano",
                Price = 15000m,
                Category = MenuCategory.Dessert,
                IsAvailable = true,
                RestaurantId = restaurants[2].Id
            }
        };

        context.MenuItems.AddRange(menuItems);
        await context.SaveChangesAsync();

        var reservations = new List<Reservation>
        {
            new()
            {
                CustomerId = customers[0].Id,
                TableId = tables[0].Id,
                ReservationDate = DateTime.UtcNow.AddDays(1).Date.AddHours(19),
                PartySize = 2,
                Status = ReservationStatus.Confirmed,
                Notes = "Aniversario"
            },
            new()
            {
                CustomerId = customers[1].Id,
                TableId = tables[3].Id,
                ReservationDate = DateTime.UtcNow.AddDays(2).Date.AddHours(20),
                PartySize = 4,
                Status = ReservationStatus.Pending,
                Notes = "Mesa cerca a la ventana"
            },
            new()
            {
                CustomerId = customers[2].Id,
                TableId = tables[6].Id,
                ReservationDate = DateTime.UtcNow.AddDays(3).Date.AddHours(18),
                PartySize = 3,
                Status = ReservationStatus.Confirmed,
                Notes = "Cumpleaños"
            }
        };

        context.Reservations.AddRange(reservations);
        await context.SaveChangesAsync();

        var orders = new List<Order>
        {
            new()
            {
                ReservationId = reservations[0].Id,
                CreatedAt = DateTime.UtcNow.AddHours(-3),
                TotalAmount = 82000m,
                SpecialInstructions = "Sin sal"
            },
            new()
            {
                ReservationId = reservations[2].Id,
                CreatedAt = DateTime.UtcNow.AddHours(-1),
                TotalAmount = 67000m,
                SpecialInstructions = "Con postre"
            }
        };

        context.Orders.AddRange(orders);
        await context.SaveChangesAsync();

        var orderItems = new List<OrderItem>
        {
            new()
            {
                OrderId = orders[0].Id,
                MenuItemId = menuItems[0].Id,
                Quantity = 1,
                UnitPrice = menuItems[0].Price
            },
            new()
            {
                OrderId = orders[0].Id,
                MenuItemId = menuItems[1].Id,
                Quantity = 2,
                UnitPrice = menuItems[1].Price
            },
            new()
            {
                OrderId = orders[1].Id,
                MenuItemId = menuItems[6].Id,
                Quantity = 1,
                UnitPrice = menuItems[6].Price
            },
            new()
            {
                OrderId = orders[1].Id,
                MenuItemId = menuItems[8].Id,
                Quantity = 1,
                UnitPrice = menuItems[8].Price
            }
        };

        context.OrderItems.AddRange(orderItems);
        await context.SaveChangesAsync();
    }
}
