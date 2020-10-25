using Azure.Cosmos;
using CarsIsland.Core.Entities;
using CarsIsland.Infrastructure.Configuration.Interfaces;
using Microsoft.Extensions.Logging;

namespace CarsIsland.Infrastructure.Data
{
    public class CarRepository : CosmosDbDataRepository<Car>
    {
        public CarRepository(ICosmosDbConfiguration cosmosDbConfiguration,
                         CosmosClient client,
                         ILogger<CarRepository> log) : base(cosmosDbConfiguration, client, log)
        {
        }

        public override string ContainerName => _cosmosDbConfiguration.CarContainerName;
    }
}
