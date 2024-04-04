namespace PizzaRestaurant.Application.Common.Interfaces.Authentication
{
    public interface IJwtGenerator
    {
        public string GenerateJwt(
                string userId,
                string username,
                string email
            );
    }
}
