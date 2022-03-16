using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using CourseManager.Settings;

namespace CourseManager.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly JWTSettings JWTSettings;
        
        public AuthenticationController(IOptions<JWTSettings> options)
        {
            JWTSettings = options.Value;
        }
        
        [HttpGet]
        public IActionResult GetToken(string login, string password)
        {
            if (string.IsNullOrEmpty(login))
            {
                return BadRequest();
            }
            
            var token = GenerateToken();

            var returnRes = new
            {
                Success = false,
                Token = token
            };

            return Ok(returnRes);
        }
        private string GenerateToken()
        {
            IList<Claim> claims = new List<Claim>();
            claims.Add(new Claim("Name", "CourseManager"));
            claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            
            var handler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JWTSettings.Secret)), SecurityAlgorithms.HmacSha256Signature),
                Audience = "https://localhost:5001",
                Issuer = "CourseManager",
                Subject = new ClaimsIdentity(claims)
            };

            SecurityToken token = handler.CreateToken(tokenDescriptor);
            
            return handler.WriteToken(token);
        }
    }
}