using UserAuthentication.Models;

namespace UserAuthentication.Repository
{
    public interface IUserRepository
    {
        Task<User> RegisterUser(User user);
        Task<Role> RegisterRole(Role role);
        Task<UserRole> RegisterUserRole(UserRole userRole);
        Task<User> AuthenticateUser(string email, string password);
    }
}
