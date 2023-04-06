using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using webapi.Libraries;
using webapi.Models;
using webapi.Models.DTOs;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthenticationController(
            UserManager<IdentityUser> userManager,
            IConfiguration configuration
            )
        {
            _configuration = configuration;
            _userManager = userManager;
        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            AuthResult authResult = new AuthResult();
            authResult.Success = false;
            
            if (!ModelState.IsValid)
            {
                authResult.Errors.Add("Invalid payload.");
                return BadRequest(authResult);
            }

            var existingUser = await _userManager.FindByNameAsync(userLoginDto.Username);
            if (existingUser == null)
            {
                authResult.Errors.Add("Invalid credentials.");
                return BadRequest(authResult);
            }
            var isValid = await _userManager.CheckPasswordAsync(existingUser, userLoginDto.Password);

            if (!isValid)
            {
                authResult.Errors.Add("Invalid credentials.");
                return BadRequest(authResult);
            }


            // generate Token
            var token = JwtGenerator.GenerateJwtToken(existingUser, _configuration);
            
            authResult.Success = true;
            authResult.Token = token;
            return Ok(authResult);
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(UserRegistrationDto userRegistrationDto)
        {
            AuthResult authResult = new AuthResult();
            authResult.Success = false;

            if (!ModelState.IsValid)
            {
                authResult.Errors.Add("Invalid payload.");
            }
            var existingUser = await _userManager.FindByEmailAsync(userRegistrationDto.Email);
            if (existingUser == null) existingUser = await _userManager.FindByNameAsync(userRegistrationDto.Username);
            if (existingUser != null)
            {
                authResult.Errors.Add("User already exists.");
                return BadRequest(authResult);
            }

            var newUser = new IdentityUser()
            {
                UserName = userRegistrationDto.Username,
                Email = userRegistrationDto.Email,
            };

            var isCreated = await _userManager.CreateAsync(newUser, userRegistrationDto.Password);
            
            if (isCreated.Succeeded)
            {
                var token = JwtGenerator.GenerateJwtToken(newUser, _configuration);
                authResult.Success = true;
                authResult.Token = token;
                return Ok(authResult);
            }

            authResult.Errors.Add("Server error.");
            return BadRequest(authResult);
        }


    }
}
