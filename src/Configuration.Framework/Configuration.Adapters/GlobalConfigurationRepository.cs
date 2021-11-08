using System;
using System.Threading;
using System.Threading.Tasks;
using Configuration.Domain.Ports;
using Database.Entities;
using Database.MongoDb.Client.Collections;
using Framework.Interfaces;
using MongoDB.Driver;

namespace Configuration.Adapters
{
    public class GlobalConfigurationRepository : IGlobalConfigurationRepository
    {
        private readonly ILoggerClient<GlobalConfigurationRepository> _logger;
        private readonly IGenericGlobalCollection<GlobalConfigurationSetting> _opsRepository;

        public GlobalConfigurationRepository(
            ILoggerClient<GlobalConfigurationRepository> logger,
            IGenericGlobalCollection<GlobalConfigurationSetting> opsRepository
        )
        {
            _logger = logger;
            _opsRepository = opsRepository;
        }

        public async Task<string> GetAsync(string key)
        {
            var collection = _opsRepository.GetCollection(false);
            var builder = Builders<GlobalConfigurationSetting>.Filter;
            var filter = builder.Eq("Schema", "GLOBAL") | builder.Eq("Key", key);
            try
            {
                var test = await collection.FindAsync(filter).ConfigureAwait(false);
                var result = (await collection.FindAsync(filter).ConfigureAwait(false)).FirstOrDefault();
                return result.Value;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting the values");
            }

            return string.Empty;
        }

        public async Task<bool> UpsertAsync(string key, string value)
        {
            var collection = _opsRepository.GetCollection(true);
            var doc = new UpdateDefinitionBuilder<GlobalConfigurationSetting>()
                .Combine(
                    Builders<GlobalConfigurationSetting>.Update.Set("schema", "GLOBAL"),
                    Builders<GlobalConfigurationSetting>.Update.Set("key", key),
                    Builders<GlobalConfigurationSetting>.Update.Set("value", value)
                );
            var builder = Builders<GlobalConfigurationSetting>.Filter;
            var filter = builder.Eq("Schema", "GLOBAL") | builder.Eq("key", key);

            var result = await collection.UpdateOneAsync(
                    filter: filter,
                    options: new UpdateOptions { IsUpsert = true },
                    update: doc)
                .ConfigureAwait(false);

            return result.IsAcknowledged;
        }
    }
}