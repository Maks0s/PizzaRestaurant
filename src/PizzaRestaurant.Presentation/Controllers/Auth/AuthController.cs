using MediatR;
using Microsoft.AspNetCore.Mvc;
using PizzaRestaurant.Presentation.Common.DTO.AuthDto;
using PizzaRestaurant.Presentation.Common.Mappers;
using PizzaRestaurant.Presentation.Controllers.Common;

namespace PizzaRestaurant.Presentation.Controllers.Auth
{
    [Route("auth")]
    public class AuthController
        : BaseApiController
    {
        private readonly IMediator _mediator;
        private readonly AuthMapper _mapper;

        public AuthController(
                IMediator mediator,
                AuthMapper mapper
            )
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<AuthResponse>> Register([FromBody] AuthRequest registerRequest)
        {
            var command = _mapper
                .MapToRegisterCommand(registerRequest);

            var registerResult =
                await _mediator.Send(command);

            return registerResult.Match(
                    registered => Created(
                            Request.Path.Value,
                            _mapper.MapToAuthResponse(registered)
                        ),
                    errors => Problem(errors)
                );

        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AuthResponse>> Login([FromBody] AuthRequest loginRequest)
        {
            var query = _mapper
                .MapToLoginQuery(loginRequest);

            var loginResult =
                await _mediator.Send(query);

            return loginResult.Match(
                    logged => Ok( 
                            _mapper.MapToAuthResponse(logged)
                        ),
                    errors => Problem(errors)
                );
        }
    }
}
