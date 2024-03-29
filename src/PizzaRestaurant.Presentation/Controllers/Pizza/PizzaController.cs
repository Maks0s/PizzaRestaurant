using MediatR;
using Microsoft.AspNetCore.Mvc;
using PizzaRestaurant.Application.Pizzas.Commands.Delete;
using PizzaRestaurant.Application.Pizzas.Queries.GetAll;
using PizzaRestaurant.Application.Pizzas.Queries.GetById;
using PizzaRestaurant.Presentation.Common.DTO;
using PizzaRestaurant.Presentation.Common.Mappers;
using PizzaRestaurant.Presentation.Controllers.Common;
using System.Text.Json.Serialization;

namespace PizzaRestaurant.Presentation.Controllers.Pizza
{
    [Route("pizza")]
    public class PizzaController : BaseApiController
    {
        private readonly IMediator _mediator;
        private readonly PizzaMapper _mapper;

        public PizzaController(IMediator mediator, PizzaMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResult> AddPizza([FromBody] PizzaRequest pizzaRequest)
        {
            var command = _mapper.MapToAddPizzaCommand(pizzaRequest);

            var creationResult =
                await _mediator.Send(command);

            return creationResult.Match(
                    created => CreatedAtAction(
                            nameof(GetPizzaById),
                            new { created.Id },
                            _mapper.MapToPizzaResponse(created)
                        ),
                    errors => Problem(errors)
                );
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<ActionResult<PizzaResponse>> GetPizzaById(Guid id)
        {
            var query = new GetPizzaByIdQuery(id);

            var queryResult =
                await _mediator.Send(query);

            return queryResult.Match<ActionResult<PizzaResponse>>(
                    received => _mapper.MapToPizzaResponse(received),
                    errors => Problem(errors)
                );
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<ICollection<PizzaResponse>>> GetAllPizzas()
        {
            var query = new GetAllPizzasQuery();

            var queryResult = 
                await _mediator.Send(query);

            return queryResult.Match(
                    received => Ok(
                            _mapper.MapToCollectionOfPizzaResponses(received)
                        ),
                    errors => Problem(errors)
                );
        }

        [HttpPut]
        [Route("update/{id:guid}")]
        public async Task<ActionResult<PizzaResponse>> UpdatePizza(Guid id, [FromBody] PizzaRequest pizzaToUpdate)
        {
            var command = _mapper.MapToUpdatePizzaCommand(pizzaToUpdate);
            command.PizzaId = id;

            var updateResult =
                await _mediator.Send(command);

            return updateResult.Match(
                    updated => Ok(
                            _mapper.MapToPizzaResponse(updated)
                        ),
                    errors => Problem(errors)
                );
        }

        [HttpDelete]
        [Route("delete/{id:guid}")]
        public async Task<ActionResult> DeletePizza(Guid id)
        {
            var command = new DeletePizzaCommand(id);

            var deleteResult =
                await _mediator.Send(command);

            return deleteResult.Match(
                    success => NoContent(),
                    errors => Problem(errors)
                );
        }
    }
}
