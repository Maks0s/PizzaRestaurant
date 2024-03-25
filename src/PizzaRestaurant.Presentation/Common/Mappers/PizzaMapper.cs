﻿using PizzaRestaurant.Application.Pizzas.Commands;
using PizzaRestaurant.Domain.Entities;
using PizzaRestaurant.Presentation.Common.DTO;
using Riok.Mapperly.Abstractions;

namespace PizzaRestaurant.Presentation.Common.Mappers
{
    [Mapper]
    public partial class PizzaMapper
    {
        public partial AddPizzaCommand MapToAddPizzaCommand(PizzaRequest pizzaRequest);

        public partial PizzaResponse MapToPizzaResponse(Pizza pizza);
    }
}