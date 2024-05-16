using CapitalTest.DTOs;
using CapitalTest.IRepositories;
using CapitalTest.IServices;
using CapitalTest.Models;
using CapitalTest.Utils;
using Microsoft.AspNetCore.Identity;

namespace CapitalTest.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IPasswordHasher<Users> _passwordHasher;
        private readonly JWT _jwt;

        public AuthService(IAuthRepository authRepository, IPasswordHasher<Users> passwordHasher, JWT jwt)
        {
            this._authRepository = authRepository;
            this._passwordHasher = passwordHasher;
            this._jwt = jwt;
        }
        public async Task<LoggedUserDto?> SignIn(LoginUserDto credintials)
        {
            Users? user = await _authRepository.GetUserByEmail(credintials.Email);

            if (user is not null)
            {
                PasswordVerificationResult result = _passwordHasher.VerifyHashedPassword(
                    user, user.Password, credintials.Password);
                if (result == PasswordVerificationResult.Success)
                {
                    var token = _jwt.GenerateToken(user);
                    var loggedUser = new LoggedUserDto
                    {
                        Id = user.Id,
                        FullName = user.FullName,
                        Email = user.Email,
                        role = user.UserRole,
                        token = token
                    };
                    return loggedUser;
                }
            }
            return null;
        }

        public async Task SignUp(Users user)
        {
            Users? exisstingsUser = await _authRepository.GetUserByEmail(user.Email);
            if (exisstingsUser is not null)
                throw new Exception("Email already exists.");

            var newUser = new Users
            {
                Id = Guid.NewGuid(),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserRole = user.UserRole,
                Password = _passwordHasher.HashPassword(user, user.Password)
            };

            await _authRepository.RegisterUser(newUser);
        }
    }
}
