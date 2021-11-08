using System.Diagnostics.CodeAnalysis;
using Framework.Helpers;
using Framework.Interfaces;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Database.MongoDb.Client
{
	public interface IMongoClientFactory
	{
		IMongoClient GetClient();
	}

	[ExcludeFromCodeCoverage]
	public class MongoClientFactory : IMongoClientFactory
	{
		private readonly IConfigurationClient _configurationClient;
		private readonly ILogger<MongoClientFactory> _logger;
		private readonly IConfigurationKeysRepository _configKeysRepo;

		private IMongoClient _client;

		public MongoClientFactory(IConfigurationClient environmentConfiguration, ILogger<MongoClientFactory> logger, IConfigurationKeysRepository configKeysRepo)
		{
			_configurationClient = environmentConfiguration;
			_logger = logger;
			_configKeysRepo = configKeysRepo;
		}

		public IMongoClient GetClient()
		{
			if (_client != null)
			{
				return _client;
			}

			var connectionString = GetReadWriteConnectionString();

			Guard.EmptyString(connectionString, nameof(connectionString));

			_client = new MongoClient(connectionString);

			return _client;
		}

		private string GetReadWriteConnectionString()
		{
			var connectionString = _configurationClient.GetValue(_configKeysRepo.MongoDbConnectionString, string.Empty);

			if (string.IsNullOrWhiteSpace(connectionString))
				_logger.LogInformation("Mongo read/write db connection string is null or whitespace");

			return connectionString;
		}
	}

}