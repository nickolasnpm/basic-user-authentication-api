using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public AuthController(IUserRepository userRepository, ITokenGenerator tokenGenerator)
        {
            _userRepository = userRepository;
            _tokenGenerator = tokenGenerator;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> registerUser (Helpers.Register register)
        {
            User? User = new User()
            {
                Id = Guid.NewGuid(),
                firstName = register.firstName,
                lastName = register.lastName,
                position = register.position,
                email = register.email,
                password = register.password,
            };

            User = await _userRepository.RegisterUser(User);

            return Ok(User);

        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> loginUser (Helpers.Login login)
        {
            User? User = await _userRepository.AuthenticateUser(login.email, login.password);

            if (User != null)
            {
                var token = await _tokenGenerator.CreateTokenAsync(User);
                return Ok(token);
            }
            return BadRequest("Email Address or Password is Incorrect");
        }
    }
}
