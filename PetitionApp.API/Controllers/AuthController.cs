using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PetitionApp.API.Configuration;
using PetitionApp.API.DTO;
using PetitionApp.Core.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PetitionApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController: ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IOptions<AuthSettings> _authSettings;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public AuthController(IMapper mapper, UserManager<User> userManager, IOptions<AuthSettings> authSettings, ILogger<AuthController> logger)
        {
            _userManager = userManager;
            _authSettings = authSettings;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterDTO register)
        {
            var user = _mapper.Map<User>(register);
            var creatingResult =  await _userManager.CreateAsync(user, register.Password);

            if (creatingResult.Succeeded)
            {
                _logger.LogInformation("Created user with username: {0}", user.UserName);
                return Created(String.Empty, String.Empty);
            }
            _logger.LogError("Error while registering new user: {0}", creatingResult.Errors.First().Description);
            return Problem(creatingResult.Errors.First().Description, null, 500);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]LoginDTO login)
        {
            var user = _userManager.Users.SingleOrDefault(user => user.UserName == login.UserName);

            if(user == null)
            {
                _logger.LogError("User with username: {0} not found", user?.UserName);
                return NotFound("User not found");
            }

            var loginResult = await _userManager.CheckPasswordAsync(user, login.Password);

            if (loginResult)
            {
                _logger.LogInformation("User with username: {0}  logIn", user.UserName);
                var token = GenerateJWT(user);
                return Ok(new { jwt_token = token });
            }

            _logger.LogError("Failed login with username: {0}. Incorrect data", user.UserName);
            return BadRequest("Login or password incorrect");
        }

        [HttpGet]
        [Route("RefreshToken")]
        [Authorize]
        public IActionResult RefreshToken()
        {
            var userid = User.Claims.FirstOrDefault(c => c.Properties.Any(p => p.Value == JwtRegisteredClaimNames.Sub))?.Value;
            if (Guid.TryParse(userid, out var id))
            {
                var user = _userManager.Users.SingleOrDefault(u => u.Id == id);
                if(user != null){
                    _logger.LogInformation("Token for user: {0}  refreshed", user.UserName);
                    var token = GenerateJWT(user);
                    return Ok( new { jwt_token = token});
                }
                _logger.LogError("Failed attemp to refresh token for user: {0}", user?.UserName);
            }

            
            return Unauthorized();
        }

        private string GenerateJWT(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authSettings.Value.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _authSettings.Value.Issuer,
                audience: _authSettings.Value.Issuer,
                claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddTicks(_authSettings.Value.TokenLifeTime.Ticks),
                signingCredentials: creds
                );
            _logger.LogInformation("Generated token for user: {0}", user.UserName);
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
