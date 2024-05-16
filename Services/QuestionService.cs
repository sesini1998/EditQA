using CapitalTest.DTOs;
using CapitalTest.IRepositories;
using CapitalTest.IServices;
using CapitalTest.Models;

namespace CapitalTest.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionsRepository _questionRepository;
        public QuestionService(IQuestionsRepository repository)
        {
            this._questionRepository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task<Questions> AddQuestion(CreateQuestionDto question)
        {
            return await _questionRepository.Add(question);
        }

        public Task<bool> DeleteQuestion(Guid questionId)
        {
            return _questionRepository.Delete(questionId);
        }

        public Task<IEnumerable<Questions>> GetQuestionsByType(QuestionType type)
        {
            return _questionRepository.GetByType(type);
        }

        public async Task<Questions?> GetQuestionById(Guid questionId)
        {
            return await _questionRepository.GetById(questionId);
        }

        public async Task<Questions?> UpdateQuestion(UpdateQuestionDto question, Guid id)
        {
            return await _questionRepository.Update(question, id);
        }
    }

}