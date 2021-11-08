using Framework.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Framework.Adapters
{
    public class ConfigurationClient : IConfigurationClient
    {
        private readonly IConfiguration _configuration;
        public ConfigurationClient(
            IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetValue(string key, string defaultValue)
        {
            var value = _configuration[key];

            if (string.IsNullOrWhiteSpace(value))
            {
                return defaultValue;
            }

            return value;
        }

        public bool TryGetValue(string key, out string value)
        {
            value = GetValue(key, string.Empty);
            return !string.IsNullOrWhiteSpace(value);
        }
    }
}