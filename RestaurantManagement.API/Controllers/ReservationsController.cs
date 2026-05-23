using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.API.DTOs.Requests;
using RestaurantManagement.API.DTOs.Responses;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Interfaces;
using RestaurantManagement.Domain.Enums;

namespace RestaurantManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    private readonly IGenericRepository<Reservation> _repository;
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Table> _tableRepository;

    public ReservationsController(
    IGenericRepository<Reservation> repository,
    IGenericRepository<Table> tableRepository,
    IMapper mapper)
    {
        _repository = repository;
        _tableRepository = tableRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReservationResponse>>> GetAll()
    {
        var reservations = await _repository.GetAllAsync();

        var response = _mapper.Map<IEnumerable<ReservationResponse>>(reservations);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ReservationResponse>> GetById(int id)
    {
        var reservation = await _repository.GetByIdAsync(id);

        if (reservation == null)
            return NotFound();

        var response = _mapper.Map<ReservationResponse>(reservation);

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<ReservationResponse>> Create(
    CreateReservationRequest request)
    {
        var table = await _tableRepository.GetByIdAsync(request.TableId);

        if (table == null)
        {
            return BadRequest("La mesa no existe.");
        }

        if (table.Status != TableStatus.Available)
        {
            return BadRequest("La mesa no está disponible.");
        }

        if (request.PartySize > table.Capacity)
        {
            return BadRequest("La cantidad de personas supera la capacidad de la mesa.");
        }

        var reservations = await _repository.GetAllAsync();

        var existsReservation = reservations.Any(r =>
            r.TableId == request.TableId &&
            r.ReservationDate == request.ReservationDate);

        if (existsReservation)
        {
            return BadRequest("La mesa ya está reservada en esa fecha.");
        }

        var reservation = _mapper.Map<Reservation>(request);

        await _repository.CreateAsync(reservation);

        table.Status = TableStatus.Reserved;

        await _tableRepository.UpdateAsync(table);

        var response = _mapper.Map<ReservationResponse>(reservation);

        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ReservationResponse>> Update(
        int id,
        UpdateReservationRequest request)
    {
        var reservation = await _repository.GetByIdAsync(id);

        if (reservation == null)
            return NotFound();

        _mapper.Map(request, reservation);

        await _repository.UpdateAsync(reservation);

        var response = _mapper.Map<ReservationResponse>(reservation);

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