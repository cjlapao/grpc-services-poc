using System;
namespace Framework.Interfaces
{
    public interface IExecutionContextService
    {
        Guid TenantId { get; set; }

        Guid CorrelationId { get; set; }
    }
}