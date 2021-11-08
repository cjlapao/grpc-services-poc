using Framework.Interfaces;

namespace Framework.Adapters
{
    public class ConfigurationKeysRepository : IConfigurationKeysRepository
    {
        public string ConfigurationServiceUrl => "CONFIGURATION_SERVICE_URL";

        public string MongoDbConnectionString => "MONGODB_CONNECTION_STRING";

        public string ServiceBusConnectionString => "SERVICE_BUS_CONNECTION_STRING";

        public string GlobalDatabaseName => "GLOBAL_DATABASE_NAME";
    }
}