using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Environment.Management.DataLayer.Entities
{
    public class BaseEntity
    {
        [BsonId]
        public Guid Id { get; set; }
    }
}