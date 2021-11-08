using System;
using Framework.Interfaces;

namespace Framework.Adapters
{
    public class ExecutionContextService : IExecutionContextService
    {
        public Guid TenantId { get; set; }
        public Guid CorrelationId { get; set; }
    }
}