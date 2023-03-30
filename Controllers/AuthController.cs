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
        private readonly DBContext _dBContext;

        public AuthController(IUserRepository userRepository, ITokenGenerator tokenGenerator, DBContext dBContext)
        {
            _userRepository = userRepository;
            _tokenGenerator = tokenGenerator;
            _dBContext = dBContext;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> registerProcess (Register register)
        {
            User? User = new User()
            { 
                firstName = register.firstName,
                lastName = register.lastName,
                emailAddress = register.emailAddress,
                password = register.password,
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
            User? User = await _userRepository.AuthenticateUser(login.emailAddress, login.password);

            if (User != null)
            {
                var token = await _tokenGenerator.CreateTokenAsync(User);
                return Ok(token);
            }
            return BadRequest("Email Address or Password is Incorrect");
        }
    }
}
