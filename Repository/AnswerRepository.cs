using CapitalTest.Data;
using CapitalTest.IRepositories;
using CapitalTest.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;

namespace CapitalTest.Repositories
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly Container _answerContainer;

        public AnswerRepository(CosmosDbService cosmosDbService)
        {
            var cosmosClient = cosmosDbService.InitializeCosmosClient();
            var databaseName = cosmosDbService.GetDatabaseName();
            var taskContainerName = "Answers";
            _answerContainer = cosmosClient.GetContainer(databaseName, taskContainerName);
        }
        public async Task<Answers?> GetById(Guid id)
        {
            var response = await _answerContainer.ReadItemAsync<Answers>(
                    id.ToString(), new PartitionKey(id.ToString()));
            return response.Resource;
        }

        public async Task<Answers?> GetByUser(Guid id)
        {
            using FeedIterator<Answers> query = _answerContainer.GetItemLinqQueryable<Answers>()
                .Where(a => a.User.Id == id)
                .ToFeedIterator();

            if (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                return response.FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        public async Task<Answers> SubmitAnswers(Answers answer)
        {
            var response = await _answerContainer.CreateItemAsync(answer);
            return response.Resource;
        }
    }
}
