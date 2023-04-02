using Microsoft.EntityFrameworkCore;
using UserAuthentication.Data;
using UserAuthentication.Helpers;
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
            user.Id = Guid.NewGuid();
            await _dBContext.UserTable.AddAsync(user);
            await _dBContext.SaveChangesAsync();
            return user;
        }

        public async Task<Role> RegisterRole(Role role)
        {
            role.Id = Guid.NewGuid();
            await _dBContext.RoleTable.AddAsync(role);
            await _dBContext.SaveChangesAsync();
            return role;
        }

        public async Task<UserRole> RegisterUserRole(UserRole userRole)
        {
            userRole.Id = Guid.NewGuid();
            await _dBContext.UserRoleTable.AddAsync(userRole);
            await _dBContext.SaveChangesAsync();
            return userRole;
        }

        public async Task<User> AuthenticateUser(string email)
        {
            User? User = await _dBContext.UserTable.FirstOrDefaultAsync(x => 
            EF.Functions.Collate(x.emailAddress, "SQL_Latin1_General_CP1_CS_AS") == email);

            if (User == null)
            {
                return null;
            }

            List<UserRole> UserRole = await _dBContext.UserRoleTable.
                Where(x => x.UserID == User.Id).ToListAsync();

            if (UserRole.Any())
            {
                User.roles = new List<string>();

                foreach (var userRole in UserRole) 
                {
                    Role? Role = await _dBContext.RoleTable.
                        FirstOrDefaultAsync(x => x.Id == userRole.RoleID);

                    if (Role != null)
                    {
                        User.roles.Add(Role.Title);
                    }
                }
            }

            return User;
        }

    }
}
