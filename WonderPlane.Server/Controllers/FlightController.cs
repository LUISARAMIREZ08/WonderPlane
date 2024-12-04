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
                //if is international available seat = 250 else 150
                AvailableSeats = flightDTO.IsInternational ? 250 : 150,
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

    [HttpGet]
    [Route("search/one-way-flexible")]
    public async Task<IActionResult> GetOneWayFlightsFlexible(
    [FromQuery] string? origin = null,
    [FromQuery] string? destination = null,
    [FromQuery] DateTime? departureDate = null,
    [FromQuery] int passengers = 1)
    {
        var responseApi = new ResponseAPI<List<Flight>>();
        try
        {
            var currentDate = DateTime.UtcNow.Date;

            var flightsQuery = _dbContext.Flights.AsQueryable();

            if (!string.IsNullOrEmpty(origin))
                flightsQuery = flightsQuery.Where(f => f.Origin == origin);
            if (!string.IsNullOrEmpty(destination))
                flightsQuery = flightsQuery.Where(f => f.Destination == destination);
            if (departureDate.HasValue)
                flightsQuery = flightsQuery.Where(f => f.DepartureDate.Date == departureDate.Value.Date);
            else
                flightsQuery = flightsQuery.Where(f => f.DepartureDate.Date >= currentDate);

            flightsQuery = flightsQuery.Where(f => f.AvailableSeats >= passengers);

            var flights = await flightsQuery.ToListAsync();

            if (!flights.Any())
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = "No hay vuelos disponibles con los criterios especificados.";
                return NotFound(responseApi);
            }

            responseApi.Data = flights;
            responseApi.EsCorrecto = true;
            responseApi.Mensaje = "Vuelos encontrados correctamente.";
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
    [Route("search/round-trip-flexible")]
    public async Task<IActionResult> GetRoundTripFlightsFlexible(
    [FromQuery] string? origin = null,
    [FromQuery] string? destination = null,
    [FromQuery] DateTime? departureDate = null,
    [FromQuery] DateTime? returnDate = null,
    [FromQuery] int passengers = 1)
    {
        var responseApi = new ResponseAPI<List<object>>();
        try
        {
            var currentDate = DateTime.UtcNow.Date;

            // Filtrar vuelos de ida
            var outboundFlightsQuery = _dbContext.Flights.AsQueryable();
            if (!string.IsNullOrEmpty(origin))
                outboundFlightsQuery = outboundFlightsQuery.Where(f => f.Origin == origin);
            if (!string.IsNullOrEmpty(destination))
                outboundFlightsQuery = outboundFlightsQuery.Where(f => f.Destination == destination);
            if (departureDate.HasValue)
                outboundFlightsQuery = outboundFlightsQuery.Where(f => f.DepartureDate.Date == departureDate.Value.Date);
            else
                outboundFlightsQuery = outboundFlightsQuery.Where(f => f.DepartureDate.Date >= currentDate);
            outboundFlightsQuery = outboundFlightsQuery.Where(f => f.AvailableSeats >= passengers);
            var outboundFlights = await outboundFlightsQuery.ToListAsync();

            // Filtrar vuelos de vuelta
            var returnFlightsQuery = _dbContext.Flights.AsQueryable();
            if (!string.IsNullOrEmpty(destination))
                returnFlightsQuery = returnFlightsQuery.Where(f => f.Origin == destination);
            if (!string.IsNullOrEmpty(origin))
                returnFlightsQuery = returnFlightsQuery.Where(f => f.Destination == origin);
            if (returnDate.HasValue)
                returnFlightsQuery = returnFlightsQuery.Where(f => f.DepartureDate.Date == returnDate.Value.Date);
            else
                returnFlightsQuery = returnFlightsQuery.Where(f => f.DepartureDate.Date >= currentDate);
            returnFlightsQuery = returnFlightsQuery.Where(f => f.AvailableSeats >= passengers);
            var returnFlights = await returnFlightsQuery.ToListAsync();

            if (!outboundFlights.Any() || !returnFlights.Any())
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = "No hay vuelos disponibles con los criterios especificados.";
                return NotFound(responseApi);
            }

            // Generar combinaciones de vuelos de ida y vuelta
            var flightPackages = new List<object>();
            foreach (var outbound in outboundFlights)
            {
                foreach (var returnFlight in returnFlights)
                {
                    // Validar que la fecha del vuelo de vuelta sea mayor a la del vuelo de ida
                    if (returnFlight.DepartureDate > outbound.DepartureDate ||
                        (returnFlight.DepartureDate == outbound.DepartureDate && returnFlight.DepartureTime > outbound.DepartureTime))
                    {
                        flightPackages.Add(new
                        {
                            OutboundFlight = outbound,
                            ReturnFlight = returnFlight
                        });
                    }
                }
            }


            // Validar si no se generaron combinaciones válidas
            if (!flightPackages.Any())
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = "No hay combinaciones válidas de vuelos de ida y vuelta.";
                return NotFound(responseApi);
            }

            responseApi.Data = flightPackages;
            responseApi.EsCorrecto = true;
            responseApi.Mensaje = "Vuelos encontrados correctamente.";
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
