using WonderPlane.Shared;
using System.Net.Http.Json;
using System.Net.Http;

namespace WonderPlane.Client.Services
{
    public class ForumService : IForumService
    {
        private readonly HttpClient _httpClient;

        public ForumService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Obtener todas las preguntas
        public async Task<List<QuestionDto>> GetQuestionsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<QuestionDto>>("api/forum/questions");
            return response ?? new List<QuestionDto>();
        }

        // Crear una nueva pregunta
        public async Task<string> CreateQuestionAsync(QuestionDto question)
        {
            var response = await _httpClient.PostAsJsonAsync("api/forum/add-question", question);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync(); // Devuelve el mensaje de éxito
            }
            else
            {
                throw new HttpRequestException($"Error al crear la pregunta: {response.StatusCode}");
            }
        }

        // Obtener una pregunta por ID
        public async Task<QuestionDto?> GetQuestionByIdAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<QuestionDto>($"api/forum/questions/{id}");
            return response;
        }

        // Crear una nueva respuesta
        public async Task<string> CreateResponseAsync(ResponseDto responseDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/forum/add-answer", responseDto);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync(); // Devuelve el mensaje de éxito
            }
            else
            {
                throw new HttpRequestException($"Error al crear la respuesta: {response.StatusCode}");
            }
        }

        // Obtener respuestas asociadas a una pregunta por ID
        public async Task<List<ResponseDto>> GetResponsesByQuestionIdAsync(int questionId)
        {
            var response = await _httpClient.GetFromJsonAsync<List<ResponseDto>>($"api/forum/questions/{questionId}/responses");
            return response ?? new List<ResponseDto>();
        }

    }
}
