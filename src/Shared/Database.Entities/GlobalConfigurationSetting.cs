using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Database.Entities
{
    public class GlobalConfigurationSetting : BaseConfigurationSetting, IConfigurationSetting
    {
        public GlobalConfigurationSetting() : base("GLOBAL") { }

        [BsonElement("key")]
        public string Key { get; set; }

        [BsonElement("value")]
        public string Value { get; set; }
    }
}