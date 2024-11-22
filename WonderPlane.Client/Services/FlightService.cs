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

        public async Task<ResponseAPI<List<FlightsFoundDTO>>> GetOneWayFlightAsync(
            string origin,
            string destination,
            DateTime departureDate,
            int passengers)
        {
            try
            {
                var queryParams = $"api/Flight/search/one-way?Origin={Uri.EscapeDataString(origin)}&Destination={Uri.EscapeDataString(destination)}&DepartureDate={departureDate:yyyy-MM-dd}&Passengers={passengers}";
                var response = await _http.GetAsync(queryParams);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<ResponseAPI<List<FlightsFoundDTO>>>();
                    return result ?? new ResponseAPI<List<FlightsFoundDTO>> { EsCorrecto = false, Mensaje = "Error deserializando respuesta." };
                }

                var errorResponse = await response.Content.ReadAsStringAsync();
                return new ResponseAPI<List<FlightsFoundDTO>>
                {
                    EsCorrecto = false,
                    Mensaje = $"Error: {response.ReasonPhrase}, Detalle: {errorResponse}"
                };
            }
            catch (Exception ex)
            {
                return new ResponseAPI<List<FlightsFoundDTO>>
                {
                    EsCorrecto = false,
                    Mensaje = $"Error inesperado: {ex.Message}"
                };
            }
        }

        public async Task<ResponseAPI<List<RoundTripDTO>>> GetRoundTripFlightsAsync(
        string origin,
        string destination,
        DateTime departureDate,
        DateTime returnDate,
        int passengers)
        {
            try
            {
                var queryParams = $"api/Flight/search/round-trip?" +
                                  $"origin={Uri.EscapeDataString(origin)}&" +
                                  $"destination={Uri.EscapeDataString(destination)}&" +
                                  $"departureDate={departureDate:yyyy-MM-dd}&" +
                                  $"returnDate={returnDate:yyyy-MM-dd}&" +
                                  $"passengers={passengers}";

                var response = await _http.GetAsync(queryParams);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<ResponseAPI<List<RoundTripDTO>>>();
                    return result ?? new ResponseAPI<List<RoundTripDTO>>
                    {
                        EsCorrecto = false,
                        Mensaje = "No se pudo deserializar la respuesta."
                    };
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                return new ResponseAPI<List<RoundTripDTO>>
                {
                    EsCorrecto = false,
                    Mensaje = $"Error {response.StatusCode}: {errorContent}"
                };
            }
            catch (Exception ex)
            {
                return new ResponseAPI<List<RoundTripDTO>>
                {
                    EsCorrecto = false,
                    Mensaje = $"Error inesperado: {ex.Message}"
                };
            }
        }

    }
}
