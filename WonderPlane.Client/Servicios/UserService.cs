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

        public async Task<string> CreateUser(RegisterDTO user)
        {
            var result = await _http.PostAsJsonAsync("api/register", user);

            if (!result.IsSuccessStatusCode)
            {
                throw new ApplicationException($"Error al registrar usuario: {result.ReasonPhrase}");
            }

            var jsonResponse = await result.Content.ReadAsStringAsync();
            Console.WriteLine($"Respuesta del servidor: {jsonResponse}"); // Aquí se imprime la respuesta del servidor
            return "pepe";
            
        }

    }
}