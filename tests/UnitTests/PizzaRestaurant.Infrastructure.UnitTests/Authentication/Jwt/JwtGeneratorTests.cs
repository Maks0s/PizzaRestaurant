using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NSubstitute;
using PizzaRestaurant.Infrastructure.Authentication.Jwt;
using PizzaRestaurant.Infrastructure.Authentication.Jwt.Common;
using PizzaRestaurant.Infrastructure.UnitTests.Authentication.Jwt.TestUtils;
using PizzaRestaurant.Infrastructure.UnitTests.Authentication.TestUtils;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace PizzaRestaurant.Infrastructure.UnitTests.Authentication.Jwt
{
    public class JwtGeneratorTests : IClassFixture<UserDataGenerator>
    {
        private readonly IOptions<JwtConfig> _mockJwtConfigOptions;
        private readonly ILogger<JwtGenerator> _mockLogger;

        private readonly JwtSecurityTokenHandler _jwtHandler;
        private readonly TokenValidationParameters _tokenValidationParameters;

        private readonly JwtGenerator _jwtGenerator;
        
        private readonly UserDataGenerator _authUserGenerator;

        public JwtGeneratorTests(UserDataGenerator authUserGenerator)
        {
            _mockJwtConfigOptions = Substitute.For<IOptions<JwtConfig>>();
            _mockJwtConfigOptions.Value
                .Returns(JwtConfigConstants.JwtConfigInstance);

            _mockLogger = Substitute.For<ILogger<JwtGenerator>>();

            _jwtHandler = new JwtSecurityTokenHandler();

            _tokenValidationParameters = new TokenValidationParameters()
            {
                ValidIssuer = JwtConfigConstants.Issuer,
                ValidateIssuer = true,

                ValidAudience = JwtConfigConstants.Audience,
                ValidateAudience = true,

                ValidateLifetime = true,

                IssuerSigningKey = new SymmetricSecurityKey(
                                    Encoding.UTF8.GetBytes(JwtConfigConstants.Key)
                                ),
                ValidateIssuerSigningKey = true
            };

            _jwtGenerator = new JwtGenerator(
                    _mockJwtConfigOptions,
                    _mockLogger
                );

            _authUserGenerator = authUserGenerator;
        }

        [Fact]
        public async Task GenerateJwt_WithValidData_ShouldReturnValidEncodedJwt()
        {
            //Arrange
            var authUser = _authUserGenerator
                .GenerateAuthUserDto();

            //Act
            var encodedJwt = _jwtGenerator
                .GenerateJwt(
                    authUser.UserId,
                    authUser.Username,
                    authUser.Email
                );

            //Assert
            var validationResult =
                await _jwtHandler
                    .ValidateTokenAsync(
                        encodedJwt,
                        _tokenValidationParameters
                    );

            validationResult.IsValid.Should().Be(true);
        }

        [Fact]
        public void GenerateJwt_WithValidData_ShouldCreateValidUserClaims()
        {
            //Arrange
            var authUser = _authUserGenerator
                .GenerateAuthUserDto();

            //Act
            var encodedJwt = _jwtGenerator
                .GenerateJwt(
                    authUser.UserId,
                    authUser.Username,
                    authUser.Email
                );

            //Assert
            var decodedJwt = _jwtHandler
                .ReadJwtToken(encodedJwt);

            decodedJwt.AssertUserClaims(authUser);
        }
    }
}
