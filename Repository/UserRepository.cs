using Microsoft.EntityFrameworkCore;
using UserAuthentication.Data;
using UserAuthentication.Models;

namespace UserAuthentication.Repository
{
    public class UserRepository: IUserRepository
    {
        private readonly DBContext _dBContext;

        public UserRepository(DBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task<User> RegisterUser(User user)
        {
            await _dBContext.userDB.AddAsync(user);
            await _dBContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> AuthenticateUser(string email, string password)
        {
            User? user = await _dBContext.userDB.FirstOrDefaultAsync(x => 
            EF.Functions.Collate(x.email, "SQL_Latin1_General_CP1_CS_AS") == email &&
            EF.Functions.Collate(x.password, "SQL_Latin1_General_CP1_CS_AS") == password);

            if (user == null)
            {
                return null;
            }

            return user;
        }
    }
}
