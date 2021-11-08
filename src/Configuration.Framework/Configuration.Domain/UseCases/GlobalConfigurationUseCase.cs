using System;
using System.Text.Json;
using System.Threading.Tasks;
using Configuration.Domain.Ports;
using Framework.Interfaces;

namespace Configuration.Domain.UseCases
{
    public class GlobalConfigurationUseCase : IGlobalConfigurationUseCase
    {
        private readonly ILoggerClient<GlobalConfigurationUseCase> _logger;
        private readonly IGlobalConfigurationRepository _globalRepository;

        public GlobalConfigurationUseCase(
            ILoggerClient<GlobalConfigurationUseCase> loggerClient,
            IGlobalConfigurationRepository globalRepository
        )
        {
            _logger = loggerClient;
            _globalRepository = globalRepository;
        }

        public async Task<T> GetAsync<T>(string schema, string flag)
        {
            var stringResult = await GetValueAsync(schema, flag, string.Empty).ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(stringResult))
            {
                return default;
            }

            try
            {
                return JsonSerializer.Deserialize<T>(stringResult);
            }
            catch
            {
                return default;
            }
        }

        public Task<bool> GetFlagAsync(string schema, string flag)
        {
            return GetFlagAsync(schema, flag, false);
        }

        public async Task<bool> GetFlagAsync(string schema, string flag, bool defaultValue)
        {
            try
            {
                return Convert.ToBoolean(await GetValueAsync(schema, flag, Convert.ToString(defaultValue)).ConfigureAwait(false));
            }
            catch
            {
                return defaultValue;
            }
        }

        public Task<string> GetValueAsync(string schema, string flag)
        {
            return GetValueAsync(schema, flag, string.Empty);
        }

        public async Task<string> GetValueAsync(string schema, string flag, string defaultValue)
        {
            switch (schema.ToLowerInvariant())
            {
                case "global":
                    try
                    {
                        return await _globalRepository.GetAsync(flag).ConfigureAwait(false);
                    }
                    catch
                    {
                        return defaultValue;
                    }
                default:
                    return defaultValue;
            }
        }

        public Task<bool> SetFlagAsync(string schema, string flag, bool value)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SetValueAsync(string schema, string flag, string value)
        {
            switch (schema.ToLowerInvariant())
            {
                case "global":
                    try
                    {
                        return await _globalRepository.UpsertAsync(flag, value).ConfigureAwait(false);
                    }
                    catch
                    {
                        return false;
                    }
                default:
                    return true;
            }
        }
    }
}