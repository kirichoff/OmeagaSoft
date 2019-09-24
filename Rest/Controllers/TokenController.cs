using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Rest.Controllers
{
    [Route("api/Token")]
    public class TokenController : Controller
    {
            SqlLiteController db;

            public TokenController(SqlLiteController context)
            {
                db = context;
            }
            [HttpGet("[action]")]
            public async Task<IActionResult> Token(string name, string password)
            {
                var identity = await GetIdentity(name, password);
                if (identity == null)
                {
                    return Unauthorized();
                }

                var now = DateTime.UtcNow;
                // создаем JWT-токен
                var jwt = new JwtSecurityToken(
                        issuer: AuthOptions.ISSUER,
                        audience: AuthOptions.AUDIENCE,
                        notBefore: now,
                        claims: identity.Claims,
                        expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                var response = new
                {
                    access_token = encodedJwt,
                    username = identity.Name
                };

                return Ok(response);
            }

            private async Task<ClaimsIdentity> GetIdentity(string userName, string password)
            {
                ClaimsIdentity identity = null;
                var user =  db.SignIn(userName,password);
                if (user != null)
                {

                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName)
                 };

                
                identity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                    
                }
                return identity;
            }
        }
    }