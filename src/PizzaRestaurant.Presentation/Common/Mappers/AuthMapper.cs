using PizzaRestaurant.Application.Auth.Commands.Register;
using PizzaRestaurant.Application.Auth.Common.AuthDto;
using PizzaRestaurant.Application.Auth.Queries.Login;
using PizzaRestaurant.Presentation.Common.DTO.AuthDto;
using Riok.Mapperly.Abstractions;

namespace PizzaRestaurant.Presentation.Common.Mappers
{
    [Mapper]
    public partial class AuthMapper
    {
        public partial RegisterCommand MapToRegisterCommand(AuthRequest authRequest);
        public partial LoginQuery MapToLoginQuery(AuthRequest authRequest);

        public partial AuthResponse MapToAuthResponse(AuthResult authResult);
    }
}
