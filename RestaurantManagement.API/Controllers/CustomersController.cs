using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.API.DTOs.Requests;
using RestaurantManagement.API.DTOs.Responses;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Interfaces;

namespace RestaurantManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly IGenericRepository<Customer> _repository;
    private readonly IMapper _mapper;

    public CustomersController(
        IGenericRepository<Customer> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustomerResponse>>> GetAll()
    {
        var customers = await _repository.GetAllAsync();

        var response = _mapper.Map<IEnumerable<CustomerResponse>>(customers);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerResponse>> GetById(int id)
    {
        var customer = await _repository.GetByIdAsync(id);

        if (customer == null)
            return NotFound();

        var response = _mapper.Map<CustomerResponse>(customer);

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<CustomerResponse>> Create(
        CreateCustomerRequest request)
    {
        var customer = _mapper.Map<Customer>(request);

        await _repository.CreateAsync(customer);

        var response = _mapper.Map<CustomerResponse>(customer);

        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CustomerResponse>> Update(
        int id,
        UpdateCustomerRequest request)
    {
        var customer = await _repository.GetByIdAsync(id);

        if (customer == null)
            return NotFound();

        _mapper.Map(request, customer);

        await _repository.UpdateAsync(customer);

        var response = _mapper.Map<CustomerResponse>(customer);

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