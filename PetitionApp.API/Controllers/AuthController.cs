using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PetitionApp.API.Configuration;
using PetitionApp.API.Models;
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
        private readonly IMapper _mapper;

        public AuthController(IMapper mapper, UserManager<User> userManager, IOptions<AuthSettings> authSettings)
        {
            _userManager = userManager;
            _authSettings = authSettings;
            _mapper = mapper;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterModel register)
        {
            var user = _mapper.Map<User>(register);
            var creatingResult =  await _userManager.CreateAsync(user, register.Password);

            if (creatingResult.Succeeded)
            {
                return Created(String.Empty, String.Empty);
            }

            return Problem(creatingResult.Errors.First().Description, null, 500);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginModel login)
        {
            var user = _userManager.Users.SingleOrDefault(user => user.UserName == login.UserName);

            if(user == null)
            {
                return NotFound("User not found");
            }

            var loginResult = await _userManager.CheckPasswordAsync(user, login.Password);

            if (loginResult)
            {
                var token = GenerateJWT(user);
                return Ok(new { jwt_token = token });
            }

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
                    var token = GenerateJWT(user);
                    return Ok( new { jwt_token = token});
                }
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

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
