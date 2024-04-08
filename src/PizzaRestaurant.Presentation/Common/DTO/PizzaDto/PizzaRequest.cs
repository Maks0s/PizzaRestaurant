namespace PizzaRestaurant.Presentation.Common.DTO.PizzaDto
{
    public record PizzaRequest(
            string Name,
            string CrustType,
            string Ingredients,
            decimal Price
        );
}
