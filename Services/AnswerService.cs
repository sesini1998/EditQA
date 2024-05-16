using CapitalTest.IRepositories;
using CapitalTest.IServices;
using CapitalTest.Models;

namespace CapitalTest.Services
{
    public class AnswerService : IAnswerService
    {
        private readonly IAnswerRepository _answerRepository;
        public AnswerService(IAnswerRepository answerRepository) 
        {
            this._answerRepository = answerRepository;
        }
        public async Task<Answers> GetAnswerById(Guid id)
        {
            return await _answerRepository.GetById(id);
        }

        public async Task<Answers> GetAnswerByUser(Guid id)
        {
            return await _answerRepository.GetByUser(id);
        }

        public async Task<Answers> SubmitAnswer(Answers answer)
        {
            return await _answerRepository.SubmitAnswers(answer);
        }
    }
}
