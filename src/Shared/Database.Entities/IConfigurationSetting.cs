using MongoDB.Bson;

namespace Database.Entities
{
    public interface IConfigurationSetting
    {
        BsonObjectId Id { get; set; }
        
        string Schema { get; }

        string Key { get; set; }

    }
}