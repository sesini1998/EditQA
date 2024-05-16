using CapitalTest.Data;
using CapitalTest.DTOs;
using CapitalTest.IRepositories;
using CapitalTest.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;

namespace CapitalTest.Repositories
{
    public class QuestionsRepository : IQuestionsRepository
    {
        private readonly Container _questionContainer;
        public QuestionsRepository(CosmosDbService cosmosDbService)
        {
            var cosmosClient = cosmosDbService.InitializeCosmosClient();
            var databaseName = cosmosDbService.GetDatabaseName();
            var taskContainerName = "Questions";
            _questionContainer = cosmosClient.GetContainer(databaseName, taskContainerName);
        }
        public async Task<Questions> Add(CreateQuestionDto question)
        {
            var createQuestion = new Questions
            {
                Id = Guid.NewGuid(),
                QuestionType = question.QuestionType,
                QuestionTitle = question.QuestionTitle,
                Visibility = question.Visibility,
                ProgramId = question.ProgramId
            };
            var response = await _questionContainer.CreateItemAsync(createQuestion);
            return response.Resource;
        }

        public async Task<IEnumerable<Questions>> GetByType(QuestionType type)
        {
            using FeedIterator<Questions> query = _questionContainer.GetItemLinqQueryable<Questions>()
                .Where(q => q.QuestionType == type)
                .ToFeedIterator();

            var questions = new List<Questions>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                questions.AddRange(response);
            }

            return questions;
        }

        public async Task<Questions?> GetById(Guid questionId)
        {
            var response = await _questionContainer.ReadItemAsync<Questions>(
                    questionId.ToString(), new PartitionKey(questionId.ToString()));
            return response.Resource;
        }

        public async Task<Questions?> Update(UpdateQuestionDto question, Guid id)
        {
           var updateQuestion = new Questions
            {
                Id = id,
                QuestionType = question.QuestionType,
                QuestionTitle = question.QuestionTitle,
                Visibility = question.Visibility,
                ProgramId = question.ProgramId
            };
            var response = await _questionContainer.ReplaceItemAsync(updateQuestion, 
                id.ToString(), new PartitionKey(id.ToString()));
            return response.Resource;
        }

        public async Task<bool> Delete(Guid questionId)
        {
           var response = await _questionContainer.DeleteItemAsync<Questions>(questionId.ToString(),
                new PartitionKey(questionId.ToString()));
            return response.StatusCode == System.Net.HttpStatusCode.NoContent;
        }
    }
}
