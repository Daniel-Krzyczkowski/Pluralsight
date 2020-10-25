using Azure.Cosmos;
using CarsIsland.Core.Entities;
using CarsIsland.Infrastructure.Configuration.Interfaces;
using Microsoft.Extensions.Logging;

namespace CarsIsland.Infrastructure.Data
{
    public class EnquiryRepository : CosmosDbDataRepository<Enquiry>
    {
        public EnquiryRepository(ICosmosDbConfiguration cosmosDbConfiguration,
                         CosmosClient client,
                         ILogger<EnquiryRepository> log) : base(cosmosDbConfiguration, client, log)
        {
        }

        public override string ContainerName => _cosmosDbConfiguration.EnquiryContainerName;
    }
}
