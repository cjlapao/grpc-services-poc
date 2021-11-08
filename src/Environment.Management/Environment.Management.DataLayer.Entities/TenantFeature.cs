using MongoDB.Bson.Serialization.Attributes;

namespace Environment.Management.DataLayer.Entities
{
    public class TenantFeature : BaseEntity
    {
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("State")]
        public TenantState State { get; set; }
    }
}