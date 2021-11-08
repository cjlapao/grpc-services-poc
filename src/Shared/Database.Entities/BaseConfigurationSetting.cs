using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Database.Entities
{
    public class BaseConfigurationSetting
    {
        [BsonId]
        public BsonObjectId Id { get; set; }
        
        [BsonElement("schema")]
        public string Schema { get; }

        public BaseConfigurationSetting(string schema)
        {
            Schema = schema;
        }
    }
}