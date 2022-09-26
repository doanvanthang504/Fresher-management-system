using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IDeviceCodeNotifier
    {
        Task OnDeviceCodeReceivedAsync(string signInUrl, string userCode);
    }
}
