using Microsoft.Extensions.DependencyInjection;

namespace Database.MongoDb.Client.Extensions
{
    public static class MongoDbClientExtensions
    {
        public static IServiceCollection AddMongoDb(this IServiceCollection services)
        {
            services.AddSingleton<IMongoClientFactory, MongoClientFactory>();
            services.AddScoped<IMongoDatabaseFactory, MongoDatabaseFactory>();

            return services;
        }
    }
}