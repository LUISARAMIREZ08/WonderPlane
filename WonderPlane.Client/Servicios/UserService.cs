﻿using WonderPlane.Shared;
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

        public async Task<string> CreateUser(UserRegisterDto user)
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


        public async Task<string> LoginUser(UserLoginDto user)
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
                //Console.WriteLine($"Token: {loginResponse.Data}");
                return loginResponse.Data;

            }

            throw new ApplicationException("Error al iniciar sesión");
        }

        public async Task<string> CreateAdmin(CreateAdminDto user)
        {
            var result = await _http.PostAsJsonAsync("api/createadmin", user);

            if (!result.IsSuccessStatusCode)
            {
                throw new ApplicationException($"Error al registrar administrador: {result.ReasonPhrase}");
            }

            var jsonResponse = await result.Content.ReadAsStringAsync();
            Console.WriteLine($"Respuesta del servidor: {jsonResponse}"); // Aquí se imprime la respuesta del servidor
            return "Administrador registrado";
        }

        public async Task<UserInfo?> GetUserById(int id)
        {
            var result = await _http.GetAsync($"api/user/{id}");

            if (result.IsSuccessStatusCode)
            {
                // Leer la respuesta y deserializarla como un objeto User
                return await result.Content.ReadFromJsonAsync<UserInfo>();
            }
            else
            {
                // Maneja el error aquí si es necesario
                var errorResponse = await result.Content.ReadFromJsonAsync<ResponseAPI<UserInfo>>();
                var errorMensaje = errorResponse?.Mensaje ?? "Error al obtener el usuario";
                throw new ApplicationException($"Error al obtener el usuario: {errorMensaje}");
            }
        }

    }
}