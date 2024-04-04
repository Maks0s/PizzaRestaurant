using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PizzaRestaurant.Application.Common.Interfaces.Authentication;
using PizzaRestaurant.Infrastructure.Authentication.Jwt.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PizzaRestaurant.Infrastructure.Authentication.Jwt
{
    public class JwtGenerator : IJwtGenerator
    {
        private readonly JwtConfig _jwtConfig;
        private readonly ILogger<JwtGenerator> _logger;

        public JwtGenerator(
                IOptions<JwtConfig> jwtConfig,
                ILogger<JwtGenerator> logger
            )
        {
            _jwtConfig = jwtConfig.Value;
            _logger = logger;
        }

        public string GenerateJwt(
                string userId,
                string username,
                string email
            )
        {
            var jwtId = Guid.NewGuid().ToString();

            var userClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, jwtId),
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(JwtRegisteredClaimNames.UniqueName, username),
                new Claim(JwtRegisteredClaimNames.Email, email)
            };

            var encodedKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_jwtConfig.Key)
                );

            var jwtDescriptor = new SecurityTokenDescriptor()
            {
                Issuer = _jwtConfig.Issuer,
                Audience = _jwtConfig.Audience,
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(
                        encodedKey,
                        SecurityAlgorithms.HmacSha256
                    ),
                Subject = new ClaimsIdentity(userClaims)
            };

            var jwtHandler = new JwtSecurityTokenHandler();

            var jwtToken = jwtHandler.CreateJwtSecurityToken(jwtDescriptor);

            _logger.LogInformation(
                    "Jwt token with ID: {jwtId} created for the user with ID: {userId}",
                    jwtId, userId
                );

            var encodedJwt = jwtHandler.WriteToken(jwtToken);
            
            return encodedJwt;
        }
    }
}
