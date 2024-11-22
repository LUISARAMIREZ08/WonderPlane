using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WonderPlane.Server.Models;
using WonderPlane.Shared;

namespace WonderPlane.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReservationController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;

    public ReservationController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> CreateReservation(Reservation reservation)
    {
        var responseApi = new ResponseAPI<int>();

        try
        {
            if (reservation.RegisteredUserId.HasValue)
            {
                var userExists = await _dbContext.Users.AnyAsync(u => u.Id == reservation.RegisteredUserId.Value);
                if (!userExists)
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = $"El usuario registrado con ID {reservation.RegisteredUserId} no existe.";
                    return BadRequest(responseApi);
                }
            }

            var newReservation = new Reservation
            {
                ReservationDate = reservation.ReservationDate,
                PaymentLimitDate = reservation.PaymentLimitDate,
                ReservationStatus = ReservationStatus.Reserved, 
                RegisteredUserId = reservation.RegisteredUserId
            };

            _dbContext.Reservations.Add(newReservation);
            await _dbContext.SaveChangesAsync();

            responseApi.Data = newReservation.Id;
            responseApi.EsCorrecto = true;
            responseApi.Mensaje = "Reserva creada correctamente.";
            return Ok(responseApi);
        }
        catch (DbUpdateException dbEx)
        {
            responseApi.EsCorrecto = false;
            responseApi.Mensaje = $"Error de base de datos: {dbEx.Message}";
            return StatusCode(500, responseApi);
        }
        catch (Exception ex)
        {
            responseApi.EsCorrecto = false;
            responseApi.Mensaje = $"Error inesperado: {ex.Message}";
            return StatusCode(500, responseApi);
        }
    }
}
