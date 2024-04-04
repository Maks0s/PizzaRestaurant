namespace PizzaRestaurant.Infrastructure.Authentication.Jwt.Common
{
    public class JwtConfig
    {
        public const string SectionName = "JwtConfig";
        public string Issuer { get; set; } = default!;
        public string Audience { get; set; } = default!;
        public string Key { get; set; } = default!;
    }
}
