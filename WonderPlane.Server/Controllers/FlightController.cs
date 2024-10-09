using WonderPlane.Server.Models;
using WonderPlane.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WonderPlane.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        public FlightController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        [Route("create")]
        public async Task<IActionResult> CreateFlight(FlightDTO flightDTO)
        {
            var responseApi = new ResponseAPI<int>();
            try
            {
                var dbFlight = new Flight
                {
                    Origin = flightDTO.Origin,
                    Destination = flightDTO.Destination,
                    DepartureDate = flightDTO.DepartureDate,
                    DepartureTime = flightDTO.DepartureTime ?? TimeSpan.Zero,
                    ArriveDate = flightDTO.ArriveDate,
                    ArriveTime = flightDTO.DepartureTime ?? TimeSpan.Zero,
                    FlightStatus = FlightStatus.Scheduled,
                    IsInternational = flightDTO.IsInternational,
                    BagPrice = flightDTO.BagPrice,
                    FlightCode = flightDTO.FlightCode,
                    Duration = flightDTO.Duration,
                    Image = flightDTO.Image,
                    PromotionId = flightDTO.PromotionId
                };
                _dbContext.Set<Flight>().Add(dbFlight);
                await _dbContext.SaveChangesAsync();
                if(dbFlight.Id > 0)
                {
                    responseApi.Data = dbFlight.Id;
                    responseApi.EsCorrecto = true;
                    responseApi.Mensaje = "Vuelo creado correctamente";
                    return Ok(responseApi);
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Error al crear el vuelo";
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }
            return Ok(responseApi);
        }
    }
}
