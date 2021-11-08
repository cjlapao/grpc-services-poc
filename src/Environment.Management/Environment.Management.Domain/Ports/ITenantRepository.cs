using System;
using System.Threading.Tasks;
using Environment.Management.Domain.Entities;

namespace Environment.Management.Domain.Ports
{
    public interface ITenantRepository
    {
        Task<Tenant> GetByIdAsync(Guid id);

        Task<bool> UpsertAsync(Tenant tenant);
    }
}