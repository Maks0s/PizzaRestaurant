namespace PizzaRestaurant.Presentation.Common.DTO
{
    public record PizzaResponse(
            string Id,
            string Name,
            string CrustType,
            string Ingredients,
            decimal Price
        );
}
