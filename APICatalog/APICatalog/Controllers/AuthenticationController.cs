using APICatalog.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace APICatalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public string SecurityAlgoritm { get; private set; }

        public AuthenticationController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            return "Authorization Controller :: Accessed in " + DateTime.Now.ToLongDateString();
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser([FromBody] UserDTO _user)
        {
            var user = new IdentityUser
            {
                UserName = _user.Email,
                Email = _user.Email,
                EmailConfirmed = true

            };

            var result = await _userManager.CreateAsync(user, _user.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            await _signInManager.SignInAsync(user, false);
            return Ok(GetToken(_user));
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UserDTO _user)
        {
            var result = await _signInManager
                .PasswordSignInAsync(_user.Email, _user.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return Ok(GetToken(_user));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Login Invalid...");
                return BadRequest(ModelState);
            }
        }

        private TokenDTO GetToken(UserDTO _user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, _user.Email),
                new Claim("pet","floquinho"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            //Creating a key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));

            // Creating digital signature

            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // var Token expire time
            var expirationHour = _configuration["TokenConfig:ExpireHours"];
            var expiration = DateTime.UtcNow.AddHours(double.Parse(expirationHour));

            // Create JWT Token
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["TokenConfig:Issuer"],
                audience: _configuration["TokenConfig:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
                );

            return new TokenDTO()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                TokenExpiration = expiration,
                Message = "Token JWT generated with Success",
                Authenticated = true
            };
        }

    }
}
