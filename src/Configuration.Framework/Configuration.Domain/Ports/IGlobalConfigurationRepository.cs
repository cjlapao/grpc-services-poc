using System.Threading.Tasks;

namespace Configuration.Domain.Ports
{
    public interface IGlobalConfigurationRepository
    {
        Task<string> GetAsync(string key);

        Task<bool> UpsertAsync(string key, string value);
    }
}