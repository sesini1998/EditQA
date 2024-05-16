using Microsoft.Azure.Cosmos;

namespace CapitalTest.Data
{
    public interface ICosmosDbService
    {
        CosmosClient InitializeCosmosClient();
        string GetDatabaseName();
    }
}
