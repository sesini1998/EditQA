using CapitalTest.Models;

namespace CapitalTest.IRepositories
{
    public interface IAuthRepository
    {
        Task<Users> RegisterUser(Users user);
        Task<Users?> GetUserByEmail(string email);
    }
}
