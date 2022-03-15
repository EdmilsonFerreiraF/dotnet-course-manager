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

namespace CourseManager.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetToken()
        {
            var token = GenerateToken();

            var returnRes = new
            {
                Token = token
            };

            return Ok(returnRes);
        }
        private string GenerateToken()
        {
            IList<Claim> claims = new List<Claim>();
            claims.Add(new Claim("Nome", "CursoManager"));
            
            var handler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes("1234567890QWERTYKQKPEQLW `l^^>:?LÓ´..Ç'")), SecurityAlgorithms.HmacSha256Signature),
                Audience = "https://localhost:5001",
                Issuer = "CourseManager",
                Subject = new ClaimsIdentity(claims)
            };

            SecurityToken token = handler.CreateToken(tokenDescriptor);
            
            return handler.WriteToken(token);
        }
    }
}