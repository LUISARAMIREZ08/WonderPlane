using WonderPlane.Shared;
using Microsoft.AspNetCore.Mvc;
using WonderPlane.Server.Models;
using Microsoft.EntityFrameworkCore;


namespace WonderPlane.Server.Controllers
{
    [Route("api")]
    [ApiController]
    public class FinanceController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public FinanceController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("addcard")]
        public async Task<ActionResult> AddCard([FromBody] CardDto cardDto)
        {
            if (cardDto == null || string.IsNullOrEmpty(cardDto.Number) || string.IsNullOrEmpty(cardDto.HolderName) || !cardDto.ExpirationDate.HasValue)
                return BadRequest("Datos de tarjeta inválidos");

            var card = new Card
            {
                Number = cardDto.Number,
                HolderName = cardDto.HolderName,
                ExpirationDate = cardDto.ExpirationDate.Value,
                SecurityCode = cardDto.SecurityCode,
                CardType = cardDto.CardTypeDto == CardTypeDto.Credit ? CardType.Credit : CardType.Debit,
                RegisteredUserId = cardDto.RegisteredUserId
            };

            _context.Cards.Add(card);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Tarjeta agregada exitosamente", CardId = card.Id });
        }

        [HttpGet("getcards-by-id")]
        public async Task<IActionResult> GetCardsByUserId(int userId)
        {
            var responseApi = new ResponseAPI<List<CardDto>>();
            try
            {
                var cards = await _context.Cards
                    .Where(c => c.RegisteredUserId == userId)
                    .ToListAsync();

                if (!cards.Any())
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No se encontraron tarjetas para el usuario especificado.";
                    return NotFound(responseApi);
                }

                var cardDtos = cards.Select(c => new CardDto
                {
                    Id = c.Id,
                    Number = c.Number,
                    HolderName = c.HolderName,
                    ExpirationDate = c.ExpirationDate,
                    CardTypeDto = c.CardType == CardType.Credit ? CardTypeDto.Credit : CardTypeDto.Debit,
                    SecurityCode = c.SecurityCode,
                    RegisteredUserId = c.RegisteredUserId
                }).ToList();

                responseApi.Data = cardDtos;
                responseApi.EsCorrecto = true;
                responseApi.Mensaje = "Lista de tarjetas obtenida correctamente.";
                return Ok(responseApi);
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = $"Error inesperado: {ex.Message}";
                return StatusCode(500, responseApi);
            }
        }

        [HttpDelete("deletecard")]
        public async Task<IActionResult> DeleteCard(int cardId)
        {
            var responseApi = new ResponseAPI<object>();
            try
            {
                var card = await _context.Cards.FindAsync(cardId);
                if (card == null)
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No se encontró la tarjeta especificada.";
                    return NotFound(responseApi);
                }

                _context.Cards.Remove(card);
                await _context.SaveChangesAsync();

                responseApi.EsCorrecto = true;
                responseApi.Mensaje = "Tarjeta eliminada correctamente.";
                return Ok(responseApi);
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = $"Error inesperado: {ex.Message}";
                return StatusCode(500, responseApi);
            }
        }

    }
}