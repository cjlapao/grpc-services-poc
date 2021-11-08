using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Configuration.Domain.Ports;
using Grpc.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Configuration.Service
{
    public class GlobalConfigurationService : Global.GlobalBase
    {
        private readonly ILogger<GlobalConfigurationService> _logger;
        private readonly IGlobalConfigurationUseCase _useCase;
        private readonly IConfiguration _config;

        public GlobalConfigurationService(
            ILogger<GlobalConfigurationService> logger,
            IConfiguration config,
            IGlobalConfigurationUseCase useCase)
        {
            _logger = logger;
            _config = config;
            _useCase = useCase;
        }

        public async override Task<GlobalFlagReply> GetFlag(GlobalFlagRequest request, ServerCallContext context)
        {
            var result = new GlobalFlagReply
            {
                Key = string.Empty,
                Value = string.Empty
            };

            if (string.IsNullOrWhiteSpace(request.FlagName))
            {
                return result;
            }

            result.Key = request.FlagName;

            result.Value = await _useCase.GetValueAsync("global", request.FlagName).ConfigureAwait(false);

            return result;
        }

        public async override Task<GlobalFlagReply> SetFlag(GlobalSetFlagRequest request, ServerCallContext context)
        {
            var result = new GlobalFlagReply
            {
                Key = string.Empty,
                Value = string.Empty
            };

            if (string.IsNullOrWhiteSpace(request.Key))
            {
                return result;
            }

            if (await _useCase.SetValueAsync("global", request.Key, request.Value).ConfigureAwait(false))
            {
                result.Key = request.Key;
                result.Value = request.Value;
            }

            return result;
        }
    }
}