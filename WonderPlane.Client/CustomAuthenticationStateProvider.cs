using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Security.Claims;
using System.Text.Json;
using WonderPlane.Client.Services;

namespace WonderPlane.Client
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly IUserService _userService;

        public CustomAuthenticationStateProvider(IJSRuntime jsRuntime, IUserService userService)
        {
            _jsRuntime = jsRuntime;
            _userService = userService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            // Aquí va el token que creamos desde el API
            string token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "Token");
            var identity = new ClaimsIdentity();

            if (!string.IsNullOrEmpty(token))
            {
                identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");

                // Obtener id del usuario
                var userIdClaim = identity.FindFirst(c => c.Type == "sub");

                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                {
                    // Obtener la información del usuario
                    var userInfo = await _userService.GetUserById(userId);
                    if (userInfo != null)
                    {
                        // Agregar todo el objeto del usuario como una claim
                        identity.AddClaim(new Claim("UserInfo", JsonSerializer.Serialize(userInfo)));

                        // imprimir el nombre del usuario en la consola
                        Console.WriteLine($"Usuario autenticado: {userInfo.UserName}");
                    }
                }
            }

            var user = new ClaimsPrincipal(identity);
            var state = new AuthenticationState(user);
            NotifyAuthenticationStateChanged(Task.FromResult(state));
            Console.WriteLine("El estado cambió");
            return state;
        }

        public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            if (keyValuePairs == null)
            {
                return Enumerable.Empty<Claim>();
            }
            return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString() ?? string.Empty));
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }

        public void NotifyUserLogout()
        {
            var identity = new ClaimsIdentity(); // Crear una identidad vacía (sin autenticación)
            var user = new ClaimsPrincipal(identity);
            var authState = Task.FromResult(new AuthenticationState(user));

            NotifyAuthenticationStateChanged(authState); // Notificar que el estado ha cambiado
        }
    }
}
