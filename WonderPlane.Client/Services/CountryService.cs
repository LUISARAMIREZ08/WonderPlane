
using System.Net.Http.Json;


namespace WonderPlane.Client.Services;
public class CountryService
{
    private readonly HttpClient _httpClient;

    public CountryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<string>> GetCountriesAsync()
    {
        // Obtener los nombres comunes de todos los países
        var response = await _httpClient.GetFromJsonAsync<List<CountryResponse>>("https://restcountries.com/v3.1/all");

        // Si la respuesta es válida, devolver la lista de nombres
        //return response?.Select(c => c.Name.Common).OrderBy(name => name).ToList() ?? new List<string>();
        return response?.Select(c => c.Name?.Common?? string.Empty).OrderBy(name => name).ToList() ?? [];
    }

    // Clase que representa la respuesta de la API
    public class CountryResponse
    {
        public Name? Name { get; set; }
    }

    public class Name
    {
        public string? Common { get; set; }
    }
}




