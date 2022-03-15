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
            var handler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {

            };

            SecurityToken token = handler.CreateToken(tokenDescriptor);
            
            return handler.WriteToken(token);
        }
    }
}