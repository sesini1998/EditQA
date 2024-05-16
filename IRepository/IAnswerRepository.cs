using CapitalTest.Models;

namespace CapitalTest.IRepositories
{
    public interface IAnswerRepository
    {
        Task<Answers?> GetById(Guid id);
        Task<Answers?> GetByUser(Guid id);
        Task<Answers> SubmitAnswers(Answers answer);
    }
}
