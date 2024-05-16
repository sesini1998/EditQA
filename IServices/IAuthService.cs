using CapitalTest.DTOs;
using CapitalTest.Models;

namespace CapitalTest.IServices
{
    public interface IAuthService
    {
        Task SignUp(Users user);
        Task<LoggedUserDto?> SignIn(LoginUserDto credintials);
    }
}
