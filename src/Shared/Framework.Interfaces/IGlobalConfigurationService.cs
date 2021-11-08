using System.Threading.Tasks;

namespace Framework.Interfaces
{
    public interface IGlobalConfigurationService
    {
        Task<string> GetValueAsync(string key);
        Task<bool> SetValueAsync(string key, string value);
    }
}