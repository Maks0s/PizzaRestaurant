using PizzaRestaurant.Application.Pizzas.Commands.Add;
using PizzaRestaurant.Application.Pizzas.Commands.Update;
using PizzaRestaurant.Domain.Entities;
using PizzaRestaurant.Presentation.Common.DTO.PizzaDto;
using Riok.Mapperly.Abstractions;

namespace PizzaRestaurant.Presentation.Common.Mappers
{
    [Mapper]
    public partial class PizzaMapper
    {
        public partial AddPizzaCommand MapToAddPizzaCommand(PizzaRequest addPizzaRequest);
        public partial UpdatePizzaCommand MapToUpdatePizzaCommand(PizzaRequest updatePizzaRequest);

        public partial PizzaResponse MapToPizzaResponse(Pizza pizza);
        public partial ICollection<PizzaResponse> MapToCollectionOfPizzaResponses(ICollection<Pizza> pizzas);
    }
}
