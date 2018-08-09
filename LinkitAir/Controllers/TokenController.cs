using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using LinkitAir.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Core.Entities;

namespace LinkitAir.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : BaseController
    {
        public TokenController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager,
                    IConfiguration configuration) : base(roleManager, userManager, configuration)
        {
        }

        /// <summary>
        /// Authenticates the user
        /// </summary>
        /// <param name="model">TokenRequestViewModel</param>
        /// <response code="200">OK</response>
        /// <response code="500">If the client payload is invalid</response>
        [HttpPost("[action]")]
        public async Task<IActionResult> Jwt([FromBody]TokenRequestViewModel model)
        {
            if (model == null) return new StatusCodeResult(500);
            switch (model.grant_type)
            {
                case "password":
                    return await GetToken(model);
                default:
                    return new UnauthorizedResult();
            }
        }

        private async Task<IActionResult> GetToken(TokenRequestViewModel model)
        {
            try
            {
                var user = await UserManager.FindByNameAsync(model.username);
                if (user == null && model.username.Contains("@"))
                    user = await UserManager.FindByEmailAsync(model.username);
                if (user == null || !await UserManager.CheckPasswordAsync(user, model.password))
                {
                    return new UnauthorizedResult();
                }

                DateTime now = DateTime.UtcNow;
                // add the registered claims for JWT (RFC7519).
                // For more info, see   https://tools.ietf.org/html/rfc7519#section-4.1
                var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,
                new DateTimeOffset(now).ToUnixTimeSeconds().ToString())
                // TODO: add additional claims here
                };

                var tokenExpirationMins =
                Configuration.GetValue<int>
                ("Auth:Jwt:TokenExpirationInMinutes");
                var issuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(Configuration["Auth:Jwt:Key"]));
                var token = new JwtSecurityToken(
                issuer: Configuration["Auth:Jwt:Issuer"],
                audience: Configuration["Auth:Jwt:Audience"],
                claims: claims,
                notBefore: now,
                expires:
                now.Add(TimeSpan.FromMinutes(tokenExpirationMins)),
                signingCredentials: new SigningCredentials(
                issuerSigningKey,
                SecurityAlgorithms.HmacSha256)
                );

                var encodedToken = new
                JwtSecurityTokenHandler().WriteToken(token);
                var response = new TokenResponseViewModel()
                {
                    token = encodedToken,
                    expiration = tokenExpirationMins
                };
                return Json(response);
            }
            catch (Exception ex)
            {
                return new UnauthorizedResult();
            }
        }


    }
}
