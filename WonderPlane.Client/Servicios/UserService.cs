using WonderPlane.Shared;
using System.Net.Http.Json;

namespace WonderPlane.Client.Servicios
{
    public class UserService : IUserService
    {
        private readonly HttpClient _http;
        //private string? _token;

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
            return "Usuario registrado";
            
        }

        public async Task<string> LoginUser(LoginDTO user)
        {
            var result = await _http.PostAsJsonAsync("api/login", user);

            if (!result.IsSuccessStatusCode)
            {
                var errorResponse = await result.Content.ReadFromJsonAsync<ResponseAPI<string>>();
                var errorMensaje = errorResponse?.Mensaje ?? "Error al iniciar sesión";
                throw new ApplicationException($"Error al iniciar sesión: {errorMensaje}");
            }

            var loginResponse = await result.Content.ReadFromJsonAsync<ResponseAPI<string>>();

            if (loginResponse != null && !string.IsNullOrEmpty(loginResponse.Data))
            {
                Console.WriteLine($"Token: {loginResponse.Data}");
                return loginResponse.Data;

            }

            throw new ApplicationException("Error al iniciar sesión");
        }

    }
}