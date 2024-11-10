using WonderPlane.Shared;
using System.Net.Http.Json;

namespace WonderPlane.Client.Services
{
    public class FinanceService : IFinanceService
    {
        private readonly HttpClient _http;

        public FinanceService(HttpClient http)
        {
            _http = http;
        }

        // Método para agregar una tarjeta
        public async Task<string> AddCard(CardDto cardDto)
        {
            var result = await _http.PostAsJsonAsync("api/addcard", cardDto);

            if (!result.IsSuccessStatusCode)
            {
                var errorResponse = await result.Content.ReadFromJsonAsync<ResponseAPI<string>>();
                var errorMensaje = errorResponse?.Mensaje ?? "Error al agregar la tarjeta";
                throw new ApplicationException($"Error al agregar la tarjeta: {errorMensaje}");
            }

            var jsonResponse = await result.Content.ReadAsStringAsync();
            Console.WriteLine($"Respuesta del servidor: {jsonResponse}"); // Aquí se imprime la respuesta del servidor
            return "Tarjeta agregada exitosamente";
        }
    }
}
