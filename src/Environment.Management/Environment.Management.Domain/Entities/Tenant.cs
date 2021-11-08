using System;
using System.Collections.Generic;

namespace Environment.Management.Domain.Entities
{
    public class Tenant : BaseEntity
    {
        public string Uri { get; set; }

        public IEnumerable<TenantFeature> Features { get; set; } = new List<TenantFeature>();
    }
}