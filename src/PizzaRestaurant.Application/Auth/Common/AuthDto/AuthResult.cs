namespace PizzaRestaurant.Application.Auth.Common.AuthDto
{
    public record AuthResult(
            string UserId,
            string EncodedJwt
        );
}
