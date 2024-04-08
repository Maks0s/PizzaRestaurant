namespace PizzaRestaurant.Presentation.Common.DTO.PizzaDto
{
    public record PizzaResponse(
            Guid Id,
            string Name,
            string CrustType,
            string Ingredients,
            decimal Price
        );
}
