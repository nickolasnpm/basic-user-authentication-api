using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserAuthentication.Data;
using UserAuthentication.Helpers;
using UserAuthentication.Models;
using UserAuthentication.Repository;

namespace UserAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IUserRepository _userRepository;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IPasswordEncryption _passwordEncryption;
        private readonly DBContext _dBContext;

        public AuthController(IUserRepository userRepository, ITokenGenerator tokenGenerator, IPasswordEncryption passwordEncryption, DBContext dBContext)
        {
            _userRepository = userRepository;
            _tokenGenerator = tokenGenerator;
            _passwordEncryption = passwordEncryption;
            _dBContext = dBContext;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> registerProcess (Register register)
        {
            _passwordEncryption.CreatePasswordHash(register.password, out byte[] PasswordHash, out byte[] PasswordSalt);

            User? User = new User()
            { 
                firstName = register.firstName,
                lastName = register.lastName,
                age = register.age,
                thisDay = DateTime.Now.ToString("g"),
                emailAddress = register.emailAddress,
                passwordHash = PasswordHash,
                passwordSalt = PasswordSalt
            };

            List<string> checkRoles = new List<string>(); 

            register.roles.ForEach(roleInput =>
            {
                checkRoles.Add(roleInput.Title);
            });

            User.roles = checkRoles;
            User = await _userRepository.RegisterUser(User); 

            List<string> checkRolesDB = new List<string>(); 

            foreach (var existingRole in _dBContext.roleDB)
            {
                checkRolesDB.Add(existingRole.Title);
            }

            Role? Role = new Role();
            UserRole? UserRole = new UserRole();

            foreach (var role in User.roles)
            {
                if (checkRolesDB.Contains(role))
                {
                    Role? roleInDatabase = await _dBContext.roleDB.FirstOrDefaultAsync(x => x.Title == role);

                    Role.Id = roleInDatabase.Id;
                    Role.Title = roleInDatabase.Title;
                }
                else
                {
                    Role.Title = role;
                    Role = await _userRepository.RegisterRole(Role);
                }
                UserRole.UserID = User.Id;
                UserRole.RoleID = Role.Id;
                UserRole = await _userRepository.RegisterUserRole(UserRole);
            }
            
            return Ok(register);

        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> loginUser (Helpers.Login login)
        {
            User? User = await _userRepository.AuthenticateUser(login.emailAddress);

            if (User != null)
            {

                if (!(_passwordEncryption.VerifyPasswordHash(login.password, User.passwordHash, User.passwordSalt)))
                {
                    return BadRequest("Wrong Password");
                }

                var token = await _tokenGenerator.CreateTokenAsync(User);
                return Ok(token);
            }
            return BadRequest("Email Address or Password is Incorrect");
        }
    }
}