using WonderPlane.Shared;
using System.Net.Http.Json;

namespace WonderPlane.Client.Servicios
{
    public class UserService : IUserService
    {
        private readonly HttpClient _http;

        public UserService(HttpClient http)
        {
            _http = http;
        }

        public async Task<RegisterDTO> CreateUser(RegisterDTO user)
        {
            var result = await _http.PostAsJsonAsync("api/register", user);

            if (!result.IsSuccessStatusCode)
            {
                throw new ApplicationException($"Error al registrar usuario: {result.ReasonPhrase}");
            }

            var jsonResponse = await result.Content.ReadAsStringAsync();
            Console.WriteLine($"Respuesta del servidor: {jsonResponse}"); // Aquí se imprime la respuesta del servidor

            // Intenta deserializar la respuesta
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<RegisterDTO>>();
            if (response!.EsCorrecto)
            {
                return response.Valor!;
            }
            else
            {
                throw new ApplicationException(response.Mensaje);
            }
        }

    }
}