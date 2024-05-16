using Microsoft.Azure.Cosmos;

namespace CapitalTest.Data
{
    public class CosmosDbService : ICosmosDbService
    {
        private readonly IConfiguration _configuration;

        public CosmosDbService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public CosmosClient InitializeCosmosClient()
        {
            var endpointUri = _configuration["CosmosDb:EndpointUri"];
            var primaryKey = _configuration["CosmosDb:PrimaryKey"];
            var databaseName = _configuration["CosmosDb:DatabaseName"];

            var cosmosClientOptions = new CosmosClientOptions
            {
                ApplicationName = databaseName,
                ConnectionMode = ConnectionMode.Gateway,
            };

            return new CosmosClient(endpointUri, primaryKey, cosmosClientOptions);
        }

        public string GetDatabaseName()
        {
            return _configuration["CosmosDb:DatabaseName"];
        }
    }

}
