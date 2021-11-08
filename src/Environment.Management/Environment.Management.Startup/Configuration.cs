using System;
using Database.MongoDb.Client;
using Database.MongoDb.Client.Collections;
using Database.MongoDb.Client.Extensions;
using Environment.Management.Domain.Profiles;
using Framework.Adapters;
using Framework.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using DbEntities = Environment.Management.DataLayer.Entities;

namespace Environment.Management
{
    public static class Configuration
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfigurationClient, ConfigurationClient>();
            services.AddSingleton<IConfigurationKeysRepository, ConfigurationKeysRepository>();
            services.AddScoped<IExecutionContextService, ExecutionContextService>();
            services.AddScoped<IGlobalConfigurationService, GlobalConfigurationService>();
            services.AddScoped(typeof(ILoggerClient<>), typeof(LoggerClient<>));
        }

        public static void AddDataLayerAdaptors(IServiceCollection services) {
            services.AddAutoMapper(typeof(Configuration).Assembly, typeof(DataLayerMapperProfiles).Assembly);

            services.AddMongoDb();
            services.AddScoped(typeof(ICollectionWrapper<>), typeof(CollectionWrapper<>));
            
            services
                .AddScoped<IGenericGlobalCollection<DbEntities.Tenant>>(x => new GenericGlobalCollection<DbEntities.Tenant>(
                    x.GetRequiredService<IMongoDatabaseFactory>(),
                    x.GetRequiredService<ICollectionWrapper<DbEntities.Tenant>>(),
                    "EMS.TenantDirectory"));
        }
    }
}