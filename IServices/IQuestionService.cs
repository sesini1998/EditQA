using CapitalTest.DTOs;
using CapitalTest.Models;

namespace CapitalTest.IServices
{
    public interface IQuestionService
    {
        Task<Questions> AddQuestion(CreateQuestionDto question);
        Task<Questions?> GetQuestionById(Guid questionId);
        Task<IEnumerable<Questions>> GetQuestionsByType(QuestionType type);
        Task<Questions?> UpdateQuestion(UpdateQuestionDto question, Guid id);
        Task<bool> DeleteQuestion(Guid questionId);
    }
}
