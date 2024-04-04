namespace PizzaRestaurant.Infrastructure.Authentication.Jwt.Common
{
    public class JwtConfig
    {
        public const string SectionName = "JwtConfig";
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }
    }
}
