using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WonderPlane.Server.Models;
using WonderPlane.Shared;

namespace WonderPlane.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PromotionController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;
    public PromotionController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    //[Authorize(Roles = "Admin")]
    [Route("create")]
    public async Task<IActionResult> CreatePromotion(PromotionDTO promotionDTO)
    {
        var responseApi = new ResponseAPI<int>();
        try
        {
            if (promotionDTO.StartDate >= promotionDTO.EndDate)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = "La fecha de inicio debe ser anterior a la fecha de fin.";
                return BadRequest(responseApi);
            }

            var promotion = new Promotion
            {
                Description = promotionDTO.Description,
                StartDate = promotionDTO.StartDate,
                EndDate = promotionDTO.EndDate,
                Discount = promotionDTO.Discount,
                PromotionStatus = promotionDTO.PromotionStatus,
                PromotionType = promotionDTO.PromotionType,
                FlightId = promotionDTO.FlightId
            };

            _dbContext.Promotions.Add(promotion);
            await _dbContext.SaveChangesAsync();

            responseApi.Data = promotion.Id;
            responseApi.EsCorrecto = true;
            responseApi.Mensaje = "Promoción creada correctamente.";
            return Ok(responseApi);
        }
        catch (DbUpdateException dbEx)
        {
            responseApi.EsCorrecto = false;
            responseApi.Mensaje = $"Error de base de datos: {dbEx.Message}";
        }
        catch (Exception ex)
        {
            responseApi.EsCorrecto = false;
            responseApi.Mensaje = $"Error inesperado: {ex.Message}";
        }

        return StatusCode(500, responseApi);
    }
}

