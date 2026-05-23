using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.API.DTOs.Requests;
using RestaurantManagement.API.DTOs.Responses;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Interfaces;

namespace RestaurantManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestaurantsController : ControllerBase
{
    private readonly IGenericRepository<Restaurant> _repository;
    private readonly IMapper _mapper;

    public RestaurantsController(
        IGenericRepository<Restaurant> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RestaurantResponse>>> GetAll()
    {
        var restaurants = await _repository.GetAllAsync();

        var response = _mapper.Map<IEnumerable<RestaurantResponse>>(restaurants);

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<RestaurantResponse>> Create(
        CreateRestaurantRequest request)
    {
        var restaurant = _mapper.Map<Restaurant>(request);

        await _repository.CreateAsync(restaurant);

        var response = _mapper.Map<RestaurantResponse>(restaurant);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RestaurantResponse>> GetById(int id)
    {
        var restaurant = await _repository.GetByIdAsync(id);

        if (restaurant == null)
            return NotFound();

        var response = _mapper.Map<RestaurantResponse>(restaurant);

        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<RestaurantResponse>> Update(
    int id,
    UpdateRestaurantRequest request)
    {
        var restaurant = await _repository.GetByIdAsync(id);

        if (restaurant == null)
            return NotFound();

        _mapper.Map(request, restaurant);

        await _repository.UpdateAsync(restaurant);

        var response = _mapper.Map<RestaurantResponse>(restaurant);

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