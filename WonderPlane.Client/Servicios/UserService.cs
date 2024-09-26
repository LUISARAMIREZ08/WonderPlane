using WonderPlane.Shared;
using System.Net.Http.Json;

namespace WonderPlane.Client.Servicios
{
    public class UserService: IUserService
    {
        private readonly HttpClient _http;
        
        public UserService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<UserDTO>> GetUsers()
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<List<UserDTO>>>("api/User");
            if (result!.EsCorrecto)
            {
                return result.Valor!;
            }
            else {
                throw new ApplicationException(result.Mensaje);
            }
        }

        public async Task<UserDTO> GetUser(int id)
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<UserDTO>>($"api/User/{id}");
            if (result!.EsCorrecto)
            {
                return result.Valor!;
            }
            else
            {
                throw new ApplicationException(result.Mensaje);
            }
        }

        public async Task<int> CreateUser(UserDTO user)
        {
            var result = await _http.PostAsJsonAsync("/register", user);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();
            if (response!.EsCorrecto)
            {
                return response.Valor!;
            }
            else
            {
                throw new ApplicationException(response.Mensaje);
            }
        }

        public Task<UserDTO> UpdateUser(UserDTO user)
        {
            throw new NotImplementedException();
        }

        public Task<UserDTO> DeleteUser(int id)
        {
            throw new NotImplementedException();
        }
    }
}
