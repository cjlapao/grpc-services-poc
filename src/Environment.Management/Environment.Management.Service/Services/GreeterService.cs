using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Framework.Interfaces;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace Environment.Management
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        private readonly IGlobalConfigurationService _globalConfigService;
        
        public GreeterService(
            ILogger<GreeterService> logger,
            IGlobalConfigurationService globalConfigurationService)
        {
            _logger = logger;
            _globalConfigService = globalConfigurationService;
        }

        public override async Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            var setting = await _globalConfigService.GetValueAsync("world").ConfigureAwait(false);

            return new HelloReply
            {
                Message = "Hello " + setting
            };
        }

        public override async Task LotsOfReplies(HelloRequest request, IServerStreamWriter<HelloReply> responseStream, ServerCallContext context)
        {
            for (var i = 0; i < 1000; i++)
            {
                _logger.LogInformation($"Saying Hello {i}");
                Thread.Sleep(100);
                await responseStream.WriteAsync(new HelloReply { Message = $"Hello {i} {request.Name}" }).ConfigureAwait(false);
            }
        }
    }
}