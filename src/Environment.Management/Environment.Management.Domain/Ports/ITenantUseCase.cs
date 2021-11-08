using System;
using System.Threading.Tasks;
using Environment.Management.Domain.Entities;

namespace Environment.Management.Domain.Ports
{
    public interface ITenantUseCase
    {
        Task CreateAsync(Tenant tenant);
    }
}