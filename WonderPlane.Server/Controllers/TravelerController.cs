using WonderPlane.Server.Models;
using WonderPlane.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WonderPlane.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TravelerController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;

    public TravelerController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> CreateTraveler([FromBody] Traveler traveler)
    {
        var responseApi = new ResponseAPI<int>();
        try
        {
            var existingTraveler = await _dbContext.Travelers
                .FirstOrDefaultAsync(t => t.Document == traveler.Document);

            if (existingTraveler != null)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = "Ya existe un viajero con este documento.";
                return BadRequest(responseApi);
            }

            if (traveler.RegisteredUserId.HasValue)
            {
                var registeredUserExists = await _dbContext.Users
                    .AnyAsync(u => u.Id == traveler.RegisteredUserId.Value);

                if (!registeredUserExists)
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "El usuario registrado especificado no existe.";
                    return BadRequest(responseApi);
                }
            }

            _dbContext.Travelers.Add(traveler);
            await _dbContext.SaveChangesAsync();

            responseApi.Data = traveler.Id;
            responseApi.EsCorrecto = true;
            responseApi.Mensaje = "Viajero creado correctamente.";
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
