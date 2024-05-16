using CapitalTest.DTOs;
using CapitalTest.Models;

namespace CapitalTest.IRepositories
{
    public interface IQuestionsRepository
    {
        Task<Questions> Add(CreateQuestionDto question);
        Task<Questions?> GetById(Guid questionId);
        Task<IEnumerable<Questions>> GetByType(QuestionType type);
        Task<Questions?> Update(UpdateQuestionDto question, Guid id);
        Task<bool> Delete(Guid questionId);
    }
}
