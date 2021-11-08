using System;
using System.Threading.Tasks;
using Database.MongoDb.Client.Collections;
using Environment.Management.Domain.Entities;
using Environment.Management.Domain.Ports;
using Framework.Interfaces;
using MongoDB.Driver;
using DbEntities = Environment.Management.DataLayer.Entities;

namespace Environment.Management.DataLayer.Adapters
{
    public class TenantRepository : ITenantRepository
    {
        private readonly ILoggerClient<TenantRepository> _logger;
        private readonly IGenericGlobalCollection<DbEntities.Tenant> _repository;

        public TenantRepository(
            ILoggerClient<TenantRepository> logger,
            IGenericGlobalCollection<DbEntities.Tenant> repository

        )
        {
            _repository = repository;
            _logger = logger;
        }

        public Task<Tenant> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpsertAsync(Tenant tenant)
        {
            var collection = _repository.GetCollection(true);
            var doc = new UpdateDefinitionBuilder<DbEntities.Tenant>()
                .Combine(
                    Builders<DbEntities.Tenant>.Update.Set("id", tenant.Id),
                    Builders<DbEntities.Tenant>.Update.Set("uri", tenant.Uri),
                    Builders<DbEntities.Tenant>.Update.Set("features", tenant.Features)
                );

            var builder = Builders<DbEntities.Tenant>.Filter;
            var filter = builder.Eq("id", tenant.Id);

            var result = await collection.UpdateOneAsync(
                    filter: filter,
                    options: new UpdateOptions { IsUpsert = true },
                    update: doc)
                .ConfigureAwait(false);

            return result.IsAcknowledged;
        }
    }
}