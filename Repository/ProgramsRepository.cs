using CapitalTest.Data;
using CapitalTest.DTOs;
using CapitalTest.IRepositories;
using CapitalTest.Models;
using Microsoft.Azure.Cosmos;

namespace CapitalTest.Repositories
{
    public class ProgramsRepository : IProgramsRepository
    {
        private readonly Container _programContainer;

        public ProgramsRepository(CosmosDbService cosmosDbService) 
        {
            var cosmosClient = cosmosDbService.InitializeCosmosClient();
            var databaseName = cosmosDbService.GetDatabaseName();
            var taskContainerName = "Questions";
            _programContainer = cosmosClient.GetContainer(databaseName, taskContainerName);
        }
        public async Task<Programs> Add(CreateProgramDto program)
        {
            var createProgram = new Programs
            {
                Id = Guid.NewGuid(),
                ProgramTitle = program.ProgramTitle,
                ProgramDescription = program.ProgramDescription,
            };
            var response = await _programContainer.CreateItemAsync(createProgram);
            return response.Resource;
        }

        public async Task<bool> Delete(Guid programId)
        {
            var response = await _programContainer.DeleteItemAsync<Programs>(programId.ToString(),
               new PartitionKey(programId.ToString()));
            return response.StatusCode == System.Net.HttpStatusCode.NoContent;
        }

        public async Task<Programs?> GetById(Guid programId)
        {
            var response = await _programContainer.ReadItemAsync<Programs>(
                   programId.ToString(), new PartitionKey(programId.ToString()));
            return response.Resource;
        }

        public async Task<Programs?> Update(UpdateProgramDto program, Guid id)
        {
            var updateProgram = new Programs
            {
                Id = id,
                ProgramTitle = program.ProgramTitle,
                ProgramDescription = program.ProgramDescription,
                Questions = program.Questions
            };
            var response = await _programContainer.ReplaceItemAsync(updateProgram,
                id.ToString(), new PartitionKey(id.ToString()));
            return response.Resource;
        }
    }
}
