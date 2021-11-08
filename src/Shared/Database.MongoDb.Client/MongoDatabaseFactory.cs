using System;
using Framework.Helpers;
using Framework.Interfaces;
using MongoDB.Driver;

namespace Database.MongoDb.Client
{
    public interface IMongoDatabaseFactory
    {
        IMongoDatabase GetTenantDatabase();
        IMongoDatabase GetGlobalDatabase();
    }

    public class MongoDatabaseFactory : IMongoDatabaseFactory
    {
        private readonly IExecutionContextService _executionContext;
        private readonly IMongoClientFactory _mongoClientFactory;

        private readonly IConfigurationKeysRepository _configurationKeysRepository;
        private readonly IConfigurationClient _configurationClient;

        public MongoDatabaseFactory(
            IExecutionContextService executionContext,
            IMongoClientFactory mongoClientFactory,
            IConfigurationKeysRepository configurationKeysRepository,
            IConfigurationClient configurationClient)
        {
            _executionContext = executionContext;
            _mongoClientFactory = mongoClientFactory;
            _configurationKeysRepository = configurationKeysRepository;
            _configurationClient = configurationClient;
        }

        public IMongoDatabase GetTenantDatabase()
        {
            var databaseName = GetDatabaseName();
            var client = _mongoClientFactory.GetClient();

            return client.GetDatabase(databaseName);
        }

        public IMongoDatabase GetGlobalDatabase()
        {
            var databaseName = _configurationClient.GetValue(_configurationKeysRepository.GlobalDatabaseName, string.Empty);
            
            Guard.EmptyString(databaseName,"Global Database Name");
            
            var client = _mongoClientFactory.GetClient();

            return client.GetDatabase(databaseName);
        }

        private string GetDatabaseName()
        {
            if (_executionContext == null)
                throw new ArgumentNullException(nameof(_executionContext), "The execution context cannot be null");

            if (_executionContext.TenantId == Guid.Empty)
                throw new ArgumentException("Value cannot be null. (Parameter 'TenantId has not been set')");

            return _executionContext.TenantId.ToString();
        }
    }

}