using System;
using System.Threading.Tasks;
using Framework.Adapters.Configuration;
using Framework.Helpers;
using Framework.Interfaces;
using Grpc.Net.Client;

namespace Framework.Adapters
{
  public class GlobalConfigurationService : IGlobalConfigurationService
  {
    private readonly IConfigurationClient _config;
    private readonly IConfigurationKeysRepository _keyRepository;
    private readonly ILoggerClient<GlobalConfigurationService> _logger;

    public GlobalConfigurationService(
      ILoggerClient<GlobalConfigurationService> loggerClient,
      IConfigurationClient configurationClient,
      IConfigurationKeysRepository configurationKeysRepository
    )
    {
      _logger = loggerClient;
      _config = configurationClient;
      _keyRepository = configurationKeysRepository;
    }

    public async Task<string> GetValueAsync(string key)
    {
      try
      {
        var configChannelUrl = _config.GetValue(_keyRepository.ConfigurationServiceUrl, string.Empty);
        Guard.EmptyString(configChannelUrl, "Config Channel Url");
        using var channel = GrpcChannel.ForAddress(configChannelUrl);
        var client = new Global.GlobalClient(channel);
        var reply = await client.GetFlagAsync(new GlobalFlagRequest { FlagName = key });

        return reply.Value;
      }
      catch (Exception ex)
      {
        _logger.Error(ex, "Error getting the config service");
      }

      return string.Empty;
    }

    public async Task<bool> SetValueAsync(string key, string value)
    {
      try
      {
        var configChannelUrl = _config.GetValue(_keyRepository.ConfigurationServiceUrl, string.Empty);
        Guard.EmptyString(configChannelUrl, "Config Channel Url");
        Guard.EmptyString(key, "Key is empty");
        Guard.EmptyString(value, "Value is empty");

        using var channel = GrpcChannel.ForAddress(configChannelUrl);
        var client = new Global.GlobalClient(channel);
        var reply = await client.SetFlagAsync(new GlobalSetFlagRequest { Key = key, Value = value });

        return !string.IsNullOrWhiteSpace(reply.Value);
      }
      catch (Exception ex)
      {
        _logger.Error(ex, "Error getting the config service");
      }

      return false;
    }
  }
}