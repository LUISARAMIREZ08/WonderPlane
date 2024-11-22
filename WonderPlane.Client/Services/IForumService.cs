using WonderPlane.Shared;

namespace WonderPlane.Client.Services
{
    public interface IForumService
    {
        Task<List<QuestionDto>> GetQuestionsAsync();
        Task<string> CreateQuestionAsync(QuestionDto question);
        Task<QuestionDto?> GetQuestionByIdAsync(int id);
        Task<string> CreateResponseAsync(ResponseDto responseDto);
        Task<List<ResponseDto>> GetResponsesByQuestionIdAsync(int questionId);
    }
}
