using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using PizzaRestaurant.Infrastructure.UnitTests.Authentication.TestUtils;
using System.IdentityModel.Tokens.Jwt;

namespace PizzaRestaurant.Infrastructure.UnitTests.Authentication.Jwt.TestUtils
{
    public static class AssertionExtesions
    {
        public static void AssertUserClaims(
                this JwtSecurityToken jwt,
                AuthUserDto authUser
            )
        {
            using var _ = new AssertionScope();

            var userIdClaim = jwt.Claims
                .FirstOrDefault(c =>
                    c.Type.Equals(JwtRegisteredClaimNames.Sub)
                );
            userIdClaim?.Value.Should().Be(authUser.UserId);

            var usernameClaim = jwt.Claims
                .FirstOrDefault(c =>
                    c.Type.Equals(JwtRegisteredClaimNames.UniqueName)
                );
            usernameClaim?.Value.Should().Be(authUser.Username);

            var userEmailClaim = jwt.Claims
                .FirstOrDefault(c =>
                    c.Type.Equals(JwtRegisteredClaimNames.Email)
                );
            userEmailClaim?.Value.Should().Be(authUser.Email);
        }
    }
}
