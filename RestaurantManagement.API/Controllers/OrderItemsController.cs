using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.API.DTOs.Requests;
using RestaurantManagement.API.DTOs.Responses;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Interfaces;

namespace RestaurantManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderItemsController : ControllerBase
{
    private readonly IGenericRepository<OrderItem> _repository;
    private readonly IMapper _mapper;

    public OrderItemsController(
        IGenericRepository<OrderItem> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderItemResponse>>> GetAll()
    {
        var items = await _repository.GetAllAsync();

        var response = _mapper.Map<IEnumerable<OrderItemResponse>>(items);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderItemResponse>> GetById(int id)
    {
        var item = await _repository.GetByIdAsync(id);

        if (item == null)
            return NotFound();

        var response = _mapper.Map<OrderItemResponse>(item);

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<OrderItemResponse>> Create(
        CreateOrderItemRequest request)
    {
        var item = _mapper.Map<OrderItem>(request);

        await _repository.CreateAsync(item);

        var response = _mapper.Map<OrderItemResponse>(item);

        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<OrderItemResponse>> Update(
        int id,
        UpdateOrderItemRequest request)
    {
        var item = await _repository.GetByIdAsync(id);

        if (item == null)
            return NotFound();

        _mapper.Map(request, item);

        await _repository.UpdateAsync(item);

        var response = _mapper.Map<OrderItemResponse>(item);

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