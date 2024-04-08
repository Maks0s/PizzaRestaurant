namespace PizzaRestaurant.Presentation.Common.DTO.AuthDto
{
    public record AuthResponse(
            string UserId,
            string EncodedJwt
        );
}
