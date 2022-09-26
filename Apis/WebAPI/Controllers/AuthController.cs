using Application.Interfaces;
using Global.Shared.ViewModels.AuthViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestViewModel request)
        {
            var token = await _authService.LoginAsync(request);
            return Ok(token);
        }
    }
}