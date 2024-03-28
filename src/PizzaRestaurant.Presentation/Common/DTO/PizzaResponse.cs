namespace PizzaRestaurant.Presentation.Common.DTO
{
    public record PizzaResponse(
            Guid Id,
            string Name,
            string CrustType,
            string Ingredients,
            decimal Price
        );
}
