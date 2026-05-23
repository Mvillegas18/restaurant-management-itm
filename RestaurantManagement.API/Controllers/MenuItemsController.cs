using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.API.DTOs.Requests;
using RestaurantManagement.API.DTOs.Responses;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Interfaces;

namespace RestaurantManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuItemsController : ControllerBase
{
    private readonly IGenericRepository<MenuItem> _repository;
    private readonly IMapper _mapper;

    public MenuItemsController(
        IGenericRepository<MenuItem> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MenuItemResponse>>> GetAll()
    {
        var items = await _repository.GetAllAsync();

        var response = _mapper.Map<IEnumerable<MenuItemResponse>>(items);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MenuItemResponse>> GetById(int id)
    {
        var item = await _repository.GetByIdAsync(id);

        if (item == null)
            return NotFound();

        var response = _mapper.Map<MenuItemResponse>(item);

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<MenuItemResponse>> Create(
        CreateMenuItemRequest request)
    {
        var item = _mapper.Map<MenuItem>(request);

        await _repository.CreateAsync(item);

        var response = _mapper.Map<MenuItemResponse>(item);

        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<MenuItemResponse>> Update(
        int id,
        UpdateMenuItemRequest request)
    {
        var item = await _repository.GetByIdAsync(id);

        if (item == null)
            return NotFound();

        _mapper.Map(request, item);

        await _repository.UpdateAsync(item);

        var response = _mapper.Map<MenuItemResponse>(item);

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