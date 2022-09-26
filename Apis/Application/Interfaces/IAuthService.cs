using Global.Shared.ViewModels.AuthViewModels;
using Global.Shared.ViewModels.TokenObjectViewModel;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<TokenObject?> LoginAsync(LoginRequestViewModel request);
    }
}