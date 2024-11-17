using WonderPlane.Server.Models;
using WonderPlane.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace WonderPlane.Server.Controllers;
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
            var existingFlight = await _dbContext.Flights
            .FirstOrDefaultAsync(f => f.FlightCode == flightDTO.FlightCode);

            if (existingFlight != null)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = $"El código de vuelo '{flightDTO.FlightCode}' ya está en uso.";
                return BadRequest(responseApi);
            }

            var dbFlight = new Flight
            {
                Origin = flightDTO.Origin,
                Destination = flightDTO.Destination,
                DepartureDate = flightDTO.DepartureDate,
                DepartureTime = TimeSpan.Parse(flightDTO.DepartureTime),
                ArriveDate = flightDTO.ArriveDate,
                ArriveTime = TimeSpan.Parse(flightDTO.ArriveTime),
                FlightStatus = FlightStatus.Scheduled,
                IsInternational = flightDTO.IsInternational,
                BagPrice = flightDTO.BagPrice,
                FlightCode = flightDTO.FlightCode,
                Duration = flightDTO.Duration,
                FirstClassPrice = flightDTO.FirstClassPrice,
                FirstClassPricePromotion = flightDTO.FirstClassPricePromotion,
                EconomicClassPrice = flightDTO.EconomicClassPrice,
                EconomicClassPricePromotion = flightDTO.EconomicClassPricePromotion,
                HasPromotion = flightDTO.HasPromotion,
                CodePromotion = flightDTO.CodePromotion,
                DiscountPercentage = flightDTO.DiscountPercentage,
                PromotionDescription = flightDTO.PromotionDescription
            };

            _dbContext.Flights.Add(dbFlight);
            await _dbContext.SaveChangesAsync();

            var seats = GenerateSeats(dbFlight, flightDTO.FirstClassPrice, flightDTO.EconomicClassPrice);
            _dbContext.Seats.AddRange(seats);
            await _dbContext.SaveChangesAsync();

            responseApi.Data = dbFlight.Id;
            responseApi.EsCorrecto = true;
            responseApi.Mensaje = "Vuelo creado correctamente";
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

        return Ok(responseApi);
    }

    [HttpGet]
    //[Authorize(Roles = "Admin")]
    [Route("list")]
    public async Task<IActionResult> GetAllFlights()
    {
        var responseApi = new ResponseAPI<List<Flight>>();
        try
        {
            var flights = await _dbContext.Flights.ToListAsync();

            responseApi.Data = flights;
            responseApi.EsCorrecto = true;
            responseApi.Mensaje = "Lista de todos los vuelos obtenida correctamente";
            return Ok(responseApi);
        }
        catch (Exception ex)
        {
            responseApi.EsCorrecto = false;
            responseApi.Mensaje = $"Error inesperado: {ex.Message}";
            return StatusCode(500, responseApi);
        }
    }

    [HttpPut]
    //[Authorize(Roles = "Admin")]
    [Route("update/{id}")]
    public async Task<IActionResult> UpdateFlight(int id, [FromBody] FlightDTO flightDTO)
    {
        var responseApi = new ResponseAPI<Flight>();

        try
        {
            var flight = await _dbContext.Flights.FindAsync(id);

            if (flight == null)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = "Vuelo no encontrado";
                return NotFound(responseApi);
            }

            var flightWithSameCode = await _dbContext.Flights
            .FirstOrDefaultAsync(f => f.FlightCode == flightDTO.FlightCode && f.Id != id);

            if (flightWithSameCode != null)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = $"El código de vuelo '{flightDTO.FlightCode}' ya está en uso.";
                return BadRequest(responseApi);
            }

            flight.Origin = flightDTO.Origin;
            flight.Destination = flightDTO.Destination;
            flight.DepartureDate = flightDTO.DepartureDate;
            flight.DepartureTime = TimeSpan.Parse(flightDTO.DepartureTime);
            flight.ArriveDate = flightDTO.ArriveDate;
            flight.ArriveTime = TimeSpan.Parse(flightDTO.ArriveTime);
            flight.IsInternational = flightDTO.IsInternational;
            flight.BagPrice = flightDTO.BagPrice;
            flight.FlightCode = flightDTO.FlightCode;
            flight.Duration = flightDTO.Duration;
            flight.FirstClassPrice = flightDTO.FirstClassPrice;
            flight.FirstClassPricePromotion = flightDTO.FirstClassPricePromotion;
            flight.EconomicClassPrice = flightDTO.EconomicClassPrice;
            flight.EconomicClassPricePromotion = flightDTO.EconomicClassPricePromotion;
            flight.FlightStatus = (FlightStatus)flightDTO.FlightStatus;
            flight.HasPromotion = flightDTO.HasPromotion;
            flight.CodePromotion = flightDTO.CodePromotion;
            flight.DiscountPercentage = flightDTO.DiscountPercentage;
            flight.PromotionDescription = flightDTO.PromotionDescription;

            await _dbContext.SaveChangesAsync();

            responseApi.Data = flight;
            responseApi.EsCorrecto = true;
            responseApi.Mensaje = "Vuelo actualizado correctamente";
            return Ok(responseApi);
        }
        catch (Exception ex)
        {
            responseApi.EsCorrecto = false;
            responseApi.Mensaje = $"Error inesperado: {ex.Message}";
            return StatusCode(500, responseApi);
        }
    }

    [HttpGet]
    //[Authorize(Roles = "Admin")]
    [Route("search/{flightCode}")]
    public async Task<IActionResult> GetFlightByCode(string flightCode)
    {
        var responseApi = new ResponseAPI<Flight>();
        try
        {
            var flight = await _dbContext.Flights
                .FirstOrDefaultAsync(f => f.FlightCode == flightCode);

            if (flight == null)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = "Vuelo no encontrado.";
                return NotFound(responseApi);
            }

            responseApi.Data = flight;
            responseApi.EsCorrecto = true;
            responseApi.Mensaje = "Vuelo encontrado correctamente.";
            return Ok(responseApi);
        }
        catch (Exception ex)
        {
            responseApi.EsCorrecto = false;
            responseApi.Mensaje = $"Error inesperado: {ex.Message}";
            return StatusCode(500, responseApi);
        }
    }

    private List<Seat> GenerateSeats(Flight flight, decimal firstClassPrice, decimal economicClassPrice)
    {
        var seats = new List<Seat>();
        int capacity = flight.IsInternational ? 250 : 150;
        int firstClassLimit = flight.IsInternational ? 50 : 25;

        for (int i = 1; i <= capacity; i++)
        {
            var seat = new Seat
            {
                Number = i,
                SeatType = i <= firstClassLimit ? SeatType.FirstClass : SeatType.Economic,
                SeatStatus = SeatStatus.Available,
                Price = i <= firstClassLimit ? firstClassPrice : economicClassPrice,
                FlightId = flight.Id
            };
            seats.Add(seat);
        }
        return seats;
    }

    [HttpGet]
    [Route("search-flight/{id}")]
    public async Task<IActionResult> SearchFlight(int id)
    {
        var responseApi = new ResponseAPI<Flight>();
        try
        {
            var flight = await _dbContext.Flights.FindAsync(id);

            if (flight == null)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = "Vuelo no encontrado.";
                return NotFound(responseApi);
            }

            responseApi.Data = flight;
            responseApi.EsCorrecto = true;
            responseApi.Mensaje = "Vuelo encontrado correctamente.";
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


