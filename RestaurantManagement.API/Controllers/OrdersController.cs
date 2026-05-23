using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.API.DTOs.Requests;
using RestaurantManagement.API.DTOs.Responses;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Interfaces;

namespace RestaurantManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IGenericRepository<Order> _repository;
    private readonly IMapper _mapper;

    public OrdersController(
        IGenericRepository<Order> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderResponse>>> GetAll()
    {
        var orders = await _repository.GetAllAsync();

        var response = _mapper.Map<IEnumerable<OrderResponse>>(orders);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderResponse>> GetById(int id)
    {
        var order = await _repository.GetByIdAsync(id);

        if (order == null)
            return NotFound();

        var response = _mapper.Map<OrderResponse>(order);

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<OrderResponse>> Create(
        CreateOrderRequest request)
    {
        var order = _mapper.Map<Order>(request);

        await _repository.CreateAsync(order);

        var response = _mapper.Map<OrderResponse>(order);

        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<OrderResponse>> Update(
        int id,
        UpdateOrderRequest request)
    {
        var order = await _repository.GetByIdAsync(id);

        if (order == null)
            return NotFound();

        _mapper.Map(request, order);

        await _repository.UpdateAsync(order);

        var response = _mapper.Map<OrderResponse>(order);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var exists = await _repository.ExistsAsync(id);

        if (!exists)
            return NotFound();

        await _repository.DeleteAsync(id);

        return NoContent();
    }
}