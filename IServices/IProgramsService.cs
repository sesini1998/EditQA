using CapitalTest.DTOs;
using CapitalTest.Models;

namespace CapitalTest.IServices
{
    public interface IProgramsService
    {
        Task<Programs> AddProgram(CreateProgramDto program);
        Task<Programs?> GetProgramById(Guid programId);
        Task<Programs?> UpdateProgram(UpdateProgramDto program, Guid id);
        Task<bool> DeleteProgram(Guid programId);
    }
}
