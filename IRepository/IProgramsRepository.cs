using CapitalTest.DTOs;
using CapitalTest.Models;

namespace CapitalTest.IRepositories
{
    public interface IProgramsRepository
    {
        Task<Programs> Add(CreateProgramDto program);
        Task<Programs?> GetById(Guid programId);
        Task<Programs?> Update(UpdateProgramDto program, Guid id);
        Task<bool> Delete(Guid programId);
    }
}
