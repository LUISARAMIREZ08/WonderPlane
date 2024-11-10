using WonderPlane.Shared;
using Microsoft.AspNetCore.Mvc;
using WonderPlane.Server.Models;


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
    }
}