using Marktguru.BusinessLogic.Configurations;
using Marktguru.BusinessLogic.Users;
using MarktguruAssignment.DataModels.Login;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MarktguruAssignment.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private JWTConfigurationSettings _jwtConfigurationSettings { get; }

        public LoginController(IOptions<JWTConfigurationSettings> config) 
        { 
            _jwtConfigurationSettings = config.Value;
        }


        [HttpPost("/Login")]
        public async Task<IActionResult> Login([FromBody] LoginCredentialsDataModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return Unauthorized();
            }

            UserRepository userRepository = new UserRepository(_jwtConfigurationSettings);

            var token = await Task.FromResult<string?>(userRepository.UserLogin(loginModel));

            if(string.IsNullOrEmpty(token))
            {
                return Unauthorized("No user found");
            }
            
            return Ok(new {token});
        }
    }
}
