using UserAuthentication.Models;

namespace UserAuthentication.Repository
{
    public interface ITokenGenerator
    {
        Task<string> CreateTokenAsync(User user);
    }
}
