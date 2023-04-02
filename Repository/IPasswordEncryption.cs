using UserAuthentication.Models;

namespace UserAuthentication.Repository
{
    public interface IPasswordEncryption
    {
        void CreatePasswordHash (string password, out byte[] passwordHash, out byte[] passwordSalt);

        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    }
}
