using CapitalTest.Models;

namespace CapitalTest.IServices
{
    public interface IAnswerService
    {
        Task<Answers> GetAnswerById(Guid id);
        Task<Answers> GetAnswerByUser(Guid id);
        Task<Answers> SubmitAnswer(Answers answer);
    }
}
