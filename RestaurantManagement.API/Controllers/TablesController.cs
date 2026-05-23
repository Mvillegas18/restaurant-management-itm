using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.API.DTOs.Requests;
using RestaurantManagement.API.DTOs.Responses;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Interfaces;

namespace RestaurantManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TablesController : ControllerBase
{
    private readonly IGenericRepository<Table> _repository;
    private readonly IMapper _mapper;

    public TablesController(
        IGenericRepository<Table> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TableResponse>>> GetAll()
    {
        var tables = await _repository.GetAllAsync();

        var response = _mapper.Map<IEnumerable<TableResponse>>(tables);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TableResponse>> GetById(int id)
    {
        var table = await _repository.GetByIdAsync(id);

        if (table == null)
            return NotFound();

        var response = _mapper.Map<TableResponse>(table);

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<TableResponse>> Create(
        CreateTableRequest request)
    {
        var table = _mapper.Map<Table>(request);

        await _repository.CreateAsync(table);

        var response = _mapper.Map<TableResponse>(table);

        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TableResponse>> Update(
        int id,
        UpdateTableRequest request)
    {
        var table = await _repository.GetByIdAsync(id);

        if (table == null)
            return NotFound();

        _mapper.Map(request, table);

        await _repository.UpdateAsync(table);

        var response = _mapper.Map<TableResponse>(table);

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