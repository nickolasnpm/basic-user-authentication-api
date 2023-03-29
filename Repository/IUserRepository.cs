using UserAuthentication.Models;

namespace UserAuthentication.Repository
{
    public interface IUserRepository
    {
        Task<User> RegisterUser(User user);

        Task<User> AuthenticateUser(string email, string password);
    }
}
