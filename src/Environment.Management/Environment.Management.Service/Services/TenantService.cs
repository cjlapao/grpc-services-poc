using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Environment.Management.Domain.Ports;
using Framework.Helpers;
using Framework.Interfaces;
using Grpc.Core;

namespace Environment.Management.Service.Services
{
    public class TenantService : Tenant.TenantBase
    {
        private readonly ILoggerClient<TenantService> _logger;
        private readonly ITenantUseCase _tenantUseCase;
        public TenantService(
            ILoggerClient<TenantService> logger,
            ITenantUseCase tenantUseCase
        )
        {
            _logger = logger;
            _tenantUseCase = tenantUseCase;
        }

        public async override Task<TenantResponse> UpsertTenant(UpsertTenantRequest request, ServerCallContext context)
        {
            Guard.EmptyString(request.Id, "id");
            Guid.TryParse(request.Id, out Guid tenantId);
            Guard.EmptyGuid(tenantId, nameof(tenantId));

            await _tenantUseCase.CreateAsync(GenerateTenant(request)).ConfigureAwait(false);

            return new TenantResponse
            {
                Id = request.Id,
                Url = request.Url
            };
        }

        private Domain.Entities.Tenant GenerateTenant(UpsertTenantRequest request)
        {
            Domain.Entities.Tenant tenant = new();
            List<Domain.Entities.TenantFeature> tenantFeatures = new List<Domain.Entities.TenantFeature>();
            tenant.Id = Guid.Parse(request.Id);
            tenant.Uri = request.Url;
            foreach (var feature in request.Features)
            {
                tenantFeatures.Add(new Domain.Entities.TenantFeature
                {
                    Id = Guid.Parse(feature.Id),
                        Name = feature.Name
                });
            }
            tenant.Features = tenantFeatures.AsEnumerable();
            return tenant;
        }
    }
}