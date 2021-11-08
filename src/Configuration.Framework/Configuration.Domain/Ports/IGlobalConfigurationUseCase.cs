using System.Threading.Tasks;

namespace Configuration.Domain.Ports
{
    public interface IGlobalConfigurationUseCase
    {
        Task<bool> GetFlagAsync(string schema, string flag);
        
        Task<bool> GetFlagAsync(string schema, string flag, bool defaultValue);

        Task<bool> SetFlagAsync(string schema, string flag, bool value);

        Task<string> GetValueAsync(string schema, string flag);

        Task<string> GetValueAsync(string schema, string flag, string defaultValue);

        Task<bool> SetValueAsync(string schema, string flag, string value);

        Task<T> GetAsync<T>(string schema, string flag);
    }
}