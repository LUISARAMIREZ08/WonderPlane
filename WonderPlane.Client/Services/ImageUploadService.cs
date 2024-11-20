using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;

namespace WonderPlane.Client.Services
{
    public class ImageUploadService
    {
        private readonly HttpClient _httpClient;

        public ImageUploadService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string?> UploadImage(IBrowserFile file)
        {
            try
            {
                // Crear contenido para la solicitud
                var content = new MultipartFormDataContent();

                // Crear un contenido del archivo y añadirlo a la solicitud
                var fileContent = new StreamContent(file.OpenReadStream(maxAllowedSize: 10_000_000)); // 10MB de tamaño máximo
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                content.Add(fileContent, "file", file.Name);

                // Hacer la solicitud POST al controlador
                var response = await _httpClient.PostAsync("api/imageUpload", content);

                // Verificar si la respuesta es exitosa
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<ImageUploadResponse>();
                    return result?.ImageUrl;
                }
                else
                {
                    return null; // Manejar error (puedes lanzar excepciones si prefieres)
                }
            }
            catch (Exception ex)
            {
                // Manejar excepciones (podrías registrar el error o mostrar un mensaje)
                Console.WriteLine($"Error al subir la imagen: {ex.Message}");
                return null;
            }
        }
    }

    public class ImageUploadResponse
    {
        public string ImageUrl { get; set; } = string.Empty;
    }
}
