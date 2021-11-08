using System.Threading.Tasks;
using Environment.Management.Domain.Entities;
using Environment.Management.Domain.Ports;

namespace Environment.Management.Domain.UseCases
{
  public class TenantUseCase : ITenantUseCase
  {
    public Task CreateAsync(Tenant tenant)
    {
      throw new System.NotImplementedException();
    }
  }
}