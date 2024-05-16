
using CapitalTest.DTOs;
using CapitalTest.IRepositories;
using CapitalTest.IServices;
using CapitalTest.Models;

namespace CapitalTest.Services
{
    public class ProgramsService : IProgramsService
    {
        private readonly IProgramsRepository _programRepository;
        public ProgramsService(IProgramsRepository repository) 
        {
            this._programRepository = repository;
        }
        public Task<Programs> AddProgram(CreateProgramDto program)
        {
            return _programRepository.Add(program);
        }

        public Task<bool> DeleteProgram(Guid programId)
        {
            return _programRepository.Delete(programId);
        }

        public Task<Programs?> GetProgramById(Guid programId)
        {
            return _programRepository.GetById(programId);
        }

        public Task<Programs?> UpdateProgram(UpdateProgramDto program, Guid id)
        {
            return _programRepository.Update(program, id);
        }
    }
}
