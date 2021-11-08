using System;
using Configuration.Adapters;
using Configuration.Domain.Ports;
using Configuration.Domain.UseCases;
using Database.Entities;
using Database.MongoDb.Client;
using Database.MongoDb.Client.Collections;
using Database.MongoDb.Client.Extensions;
using Framework.Adapters;
using Framework.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Configuration.Startup
{
    public static class Startup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfigurationClient, ConfigurationClient>();
            services.AddSingleton<IConfigurationKeysRepository, ConfigurationKeysRepository>();
            services.AddScoped<IExecutionContextService, ExecutionContextService>();

            services.AddScoped<IGlobalConfigurationUseCase, GlobalConfigurationUseCase>();
            services.AddScoped<IGlobalConfigurationRepository, GlobalConfigurationRepository>();

            services.AddScoped(typeof(ILoggerClient<>), typeof(LoggerClient<>));
            AddGlobalCollections(services);
        }

        private static void AddGlobalCollections(IServiceCollection services)
        {
            services.AddMongoDb();
            services.AddScoped(typeof(ICollectionWrapper<>), typeof(CollectionWrapper<>));
            
            services
                .AddScoped<IGenericGlobalCollection<GlobalConfigurationSetting>>(x => new GenericGlobalCollection<GlobalConfigurationSetting>(
                    x.GetRequiredService<IMongoDatabaseFactory>(),
                    x.GetRequiredService<ICollectionWrapper<GlobalConfigurationSetting>>(),
                    "Ops"));
        }
    }
}