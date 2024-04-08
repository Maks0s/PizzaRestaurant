namespace PizzaRestaurant.Presentation.Common.DTO.AuthDto
{
    public record AuthRequest(
            string Username,
            string Email,
            string Password
        );
}
