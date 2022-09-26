using Application.Interfaces;
using Domain.Entities;
using Global.Shared.Helpers;
using Global.Shared.ViewModels.AuthViewModels;
using Global.Shared.ViewModels.TokenObjectViewModel;
using System;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;

        public AuthService(IUnitOfWork unitOfWork, IJwtService jwtService)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
        }

        public async Task<TokenObject?> LoginAsync(LoginRequestViewModel request)
        {
            var user = await AuthenticateAsync(request);

            var token = _jwtService.GenerateToken(user);

            return token;
        }

        private async Task<User> AuthenticateAsync(LoginRequestViewModel request)
        {
            bool hasEmail = !string.IsNullOrEmpty(request.Email);

            var user = hasEmail ? await _unitOfWork
                                    .UserRepository
                                    .GetByEmailAsync(request.Email!)
                                : await _unitOfWork
                                    .UserRepository
                                    .GetByUsernameAsync(request.Username!);

            if (user is null)
                throw new Exception(ExceptionMessages.Auth.USER_NOT_FOUND);

            var passwordVerificationResult = PasswordHasher.Compare(request.Password!, user.HashedPassword!);
            if (passwordVerificationResult != PasswordVerificationResult.Success)
                throw new Exception(ExceptionMessages.Auth.USER_NOT_FOUND);

            return user;
        }
    }
}
