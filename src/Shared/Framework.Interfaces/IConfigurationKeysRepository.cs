namespace Framework.Interfaces
{
    public interface IConfigurationKeysRepository
    {
        string ConfigurationServiceUrl { get; }
        string MongoDbConnectionString { get; }

        string ServiceBusConnectionString { get; }

        string GlobalDatabaseName { get; }
    }
}