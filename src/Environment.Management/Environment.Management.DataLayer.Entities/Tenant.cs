using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace Environment.Management.DataLayer.Entities
{
    public class Tenant : BaseEntity
    {
        [BsonElement("uri")]
        public string Uri { get; set; }

        [BsonElement("features")]
        public IEnumerable<TenantFeature> Features { get; set; } = new List<TenantFeature>();
    }
}