namespace PizzaRestaurant.Presentation.Common.DTO
{
    public record PizzaRequest(
            string Name,
            string CrustType,
            string Ingredients,
            decimal Price
        );
}
