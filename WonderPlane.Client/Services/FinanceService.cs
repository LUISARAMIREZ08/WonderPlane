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

        // Método para obtener las tarjetas de un usuario
        public async Task<List<CardDto>> GetCardsByUserId(int userId)
        {
            var response = await _http.GetFromJsonAsync<ResponseAPI<List<CardDto>>>($"api/getcards-by-id?userId={userId}");

            if (response == null || !response.EsCorrecto)
            {
                var errorMensaje = response?.Mensaje ?? "Error al obtener las tarjetas";
                throw new ApplicationException($"Error al obtener las tarjetas: {errorMensaje}");
            }

            return response.Data ?? new List<CardDto>();
        }

        // Método para eliminar una tarjeta por su id
        public async Task<string> DeleteCardAsync(int cardId)
        {
            var response = await _http.DeleteAsync($"api/deletecard?cardId={cardId}");

            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<ResponseAPI<object>>();
                var errorMensaje = errorResponse?.Mensaje ?? "Error al eliminar la tarjeta";
                throw new ApplicationException($"Error al eliminar la tarjeta: {errorMensaje}");
            }

            return "Tarjeta eliminada correctamente";
        }


    }
}
