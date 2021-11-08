namespace Framework.Interfaces
{
    public interface IConfigurationClient
    {
        string GetValue(string key, string defaultValue);

        bool TryGetValue(string key, out string value);
    }
}