namespace CarsIsland.Infrastructure.Configuration.Interfaces
{
    public interface ICosmosDbConfiguration
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string CarContainerName { get; set; }
        string EnquiryContainerName { get; set; }
        string PartitionKeyPath { get; set; }
    }
}
