using Core.Entities.Identity;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Core.Services
{
    /// <summary>
    /// Сервіс для генерування токенів
    /// </summary>
    public class TokenService(IConfiguration configuration, UserManager<User> userManager, ILogger<TokenService> logger) : ITokenService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly UserManager<User> _userManager = userManager;
        private readonly ILogger<TokenService> _logger = logger;

        /// <summary>
        /// {Interface} Описано в інтерфейсі ITokenService
        /// </summary>
        public async Task<string> CreateToken(User user)
        {
            return await Task.Run(() =>
            {
                var roles = _userManager.GetRolesAsync(user).Result;

                List<Claim> claims =
                [
                    new Claim("id", user.Id.ToString()),
                    new Claim("email", user.Email),
                    new Claim("firstName", user.FirstName),
                    new Claim("secondName", user.SecondName),
                    new Claim("photo", user.Photo),
                ];

                if (roles.Any())
                    foreach (var role in roles)
                        claims.Add(new Claim("roles", role));

                var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<String>("JwtKey")));
                var signinCredentials = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256);

                var jwt = new JwtSecurityToken(
                    signingCredentials: signinCredentials,
                    expires: DateTime.Now.AddDays(1),
                    claims: claims
                );

                return new JwtSecurityTokenHandler().WriteToken(jwt);
            });
        }
        /// <summary>
        /// {Interface} Описано в інтерфейсі ITokenService
        /// </summary>
        public async Task<bool> ValidateToken(string token)
        {
            return await Task.Run(() =>
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters()
                {
                    ValidateLifetime = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidIssuer = "Sample",
                    ValidAudience = "Sample",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<String>("JwtKey")))
                };

                try
                {
                    IPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }
    }
}
