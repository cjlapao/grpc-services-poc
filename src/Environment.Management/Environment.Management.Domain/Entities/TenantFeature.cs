namespace Environment.Management.Domain.Entities
{
    public class TenantFeature : BaseEntity
    {
        public string Name { get; set; }

        public TenantState State { get; set; }
    }
}