using Azure.Cosmos;
using Azure.Cosmos.Serialization;
using CarsIsland.Core.Entities;
using CarsIsland.Core.Interfaces;
using CarsIsland.Infrastructure.Configuration.Interfaces;
using CarsIsland.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace CarsIsland.API.Core.DependencyInjection
{
    public static class DataServiceCollectionExtensions
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();

            var cosmoDbConfiguration = serviceProvider.GetRequiredService<ICosmosDbConfiguration>();
            CosmosClientOptions cosmosClientOptions = new CosmosClientOptions
            {
                SerializerOptions = new CosmosSerializationOptions()
                {
                    PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
                }
            };
            CosmosClient cosmosClient = new CosmosClient(cosmoDbConfiguration.ConnectionString, cosmosClientOptions);
            CosmosDatabase database = cosmosClient.CreateDatabaseIfNotExistsAsync(cosmoDbConfiguration.DatabaseName)
                                                   .GetAwaiter()
                                                   .GetResult();
            CosmosContainer container = database.CreateContainerIfNotExistsAsync(
                cosmoDbConfiguration.ContainerName,
                cosmoDbConfiguration.PartitionKeyPath,
                400)
                .GetAwaiter()
                .GetResult();

            services.AddSingleton(cosmosClient);

            services.AddSingleton<IDataRepository<Car>, CosmosDbDataRepository<Car>>();
            services.AddSingleton<IDataRepository<Enquiry>, CosmosDbDataRepository<Enquiry>>();

            return services;
        }
    }
}
