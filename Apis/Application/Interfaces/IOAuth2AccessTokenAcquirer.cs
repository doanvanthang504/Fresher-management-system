using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IOAuth2AccessTokenAcquirer
    {
        Task<string> GetAccessTokenAsync(
            IEnumerable<string> scopes,
            string username,
            IDeviceCodeNotifier deviceCodeNotifier);
    }
}
