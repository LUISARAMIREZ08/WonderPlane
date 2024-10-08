using WonderPlane.Shared;
using System.Net.Http.Json;

namespace WonderPlane.Client.Servicios
{
    public class FlightService : IFlightService
    {
        private readonly HttpClient _http;

        public FlightService(HttpClient http)
        {
            _http = http;
        }

        public async Task<string> CreateFlight(FlightDTO flight)
        {
            var result = await _http.PostAsJsonAsync("api/flight/create", flight);

            if (!result.IsSuccessStatusCode)
            {
                throw new ApplicationException($"Error al crear vuelo: {result.ReasonPhrase}");
            }

            var jsonResponse = await result.Content.ReadAsStringAsync();
            Console.WriteLine($"Respuesta del servidor: {jsonResponse}"); 
            return "Vuelo creado";
        }
    }
}
