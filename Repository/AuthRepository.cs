using CapitalTest.Data;
using CapitalTest.IRepositories;
using CapitalTest.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;

namespace CapitalTest.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly Container _userContainer;

        public AuthRepository(CosmosDbService cosmosDbService)
        {
            var cosmosClient = cosmosDbService.InitializeCosmosClient();
            var databaseName = cosmosDbService.GetDatabaseName();
            var taskContainerName = "Users";
            _userContainer = cosmosClient.GetContainer(databaseName, taskContainerName);
        }
        public async Task<Users?> GetUserByEmail(string email)
        {
            using FeedIterator<Users> query = _userContainer.GetItemLinqQueryable<Users>()
               .Where(u => u.Email == email)
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

        public async Task<Users> RegisterUser(Users user)
        {
            var response = await _userContainer.CreateItemAsync(user);
            return response.Resource;
        }
    }
}
