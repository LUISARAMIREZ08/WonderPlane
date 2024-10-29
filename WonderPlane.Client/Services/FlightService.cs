using WonderPlane.Shared;
using System.Net.Http.Json;
using System.Net.Http;

namespace WonderPlane.Client.Services
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
            var response = await _http.PostAsJsonAsync("api/flight/create", flight);

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException($"Error al crear vuelo: {response.ReasonPhrase}");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Respuesta del servidor: {jsonResponse}");
            return "Vuelo creado";
        }

        public async Task<FlightDTO?> SearchFlight(int id)
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<FlightDTO>>($"api/flight/search-flight/{id}");

            if (result!.EsCorrecto)
                return result.Data; 
            else
                throw new Exception(result.Mensaje);
        }

        public async Task<List<FlightDTO>> ListFlight()
        {
            var response = await _http.GetAsync("api/flight/list");

            if (response.IsSuccessStatusCode)
            {
                var responseApi = await response.Content.ReadFromJsonAsync<ResponseAPI<List<FlightDTO>>>();
                return responseApi?.Data ?? new List<FlightDTO>();
            }
            else
            {
                return new List<FlightDTO>();
            }
        }

        public async Task<ResponseAPI<FlightDTO>> UpdateFlight(int id, FlightDTO flightDTO)
        {
            var response = await _http.PutAsJsonAsync($"api/flight/update/{id}", flightDTO);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ResponseAPI<FlightDTO>>();
            }
            else
            {
                return new ResponseAPI<FlightDTO>
                {
                    EsCorrecto = false,
                    Mensaje = "Error al actualizar el vuelo."
                };
            }
        }

        public async Task<ResponseAPI<FlightDTO>> GetFlightByCode(string flightCode)
        {
            var response = await _http.GetAsync($"api/flight/search/{flightCode}");

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new ApplicationException($"Error al buscar vuelo: {response.ReasonPhrase}, Detalles: {errorMessage}");
            }

            var jsonResponse = await response.Content.ReadFromJsonAsync<ResponseAPI<FlightDTO>>();

            if (jsonResponse == null || !jsonResponse.EsCorrecto)
            {
                throw new ApplicationException($"Error inesperado: {jsonResponse?.Mensaje}");
            }

            Console.WriteLine($"Respuesta del servidor: {jsonResponse.Mensaje}");
            return jsonResponse;
        }
    
}
}
