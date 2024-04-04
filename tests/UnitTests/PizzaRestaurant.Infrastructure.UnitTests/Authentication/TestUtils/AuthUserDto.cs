namespace PizzaRestaurant.Infrastructure.UnitTests.Authentication.TestUtils
{
    public record AuthUserDto(
            string UserId,
            string Username,
            string Email
        );
}
