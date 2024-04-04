using PizzaRestaurant.Infrastructure.Authentication.Jwt.Common;

namespace PizzaRestaurant.Infrastructure.UnitTests.Authentication.Jwt.TestUtils
{
    public static class JwtConfigConstants
    {
        public const string Issuer = "JwtGeneratorUnitTests.Issuer";
        public const string Audience = "JwtGeneratorUnitTests.Audience";
        public const string Key = "ZetfS32hEXRF9cq67J7qY1Giji3BejAxWvL2XS+PepXJ/+Q6FePtmFzVTcLmXuXi";

        public static readonly JwtConfig JwtConfigInstance =
            new JwtConfig()
            {
                Issuer = Issuer,
                Audience = Audience,
                Key = Key
            };
    }
}
