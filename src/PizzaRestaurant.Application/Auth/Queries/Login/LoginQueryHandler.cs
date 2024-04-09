using ErrorOr;
using Microsoft.AspNetCore.Identity;
using PizzaRestaurant.Application.Auth.Common.AuthDto;
using PizzaRestaurant.Application.Common.AppErrors;
using PizzaRestaurant.Application.Common.Interfaces.Authentication;
using PizzaRestaurant.Application.Common.Interfaces.CQRS;
using PizzaRestaurant.Domain.Entities;

namespace PizzaRestaurant.Application.Auth.Queries.Login
{
    public class LoginQueryHandler
        : IQueryHandler<LoginQuery, AuthResult>
    {
        private readonly IJwtGenerator _jwtGenerator;
        private readonly UserManager<AuthUser> _userManager;

        public LoginQueryHandler(
                IJwtGenerator jwtGenerator,
                UserManager<AuthUser> userManager
            )
        {
            _jwtGenerator = jwtGenerator;
            _userManager = userManager;
        }

        public async Task<ErrorOr<AuthResult>> Handle(
                LoginQuery query,
                CancellationToken cancellationToken
            )
        {
            var registeredUser =
                await _userManager
                    .FindByEmailAsync(query.Email);

            if (registeredUser is null)
            {
                return Errors.Auth.InvalidCredentials();
            }

            var isPasswordValid =
                await _userManager
                    .CheckPasswordAsync(
                        registeredUser,
                        query.Password
                    );

            if(!isPasswordValid)
            {
                return Errors.Auth.InvalidCredentials();
            }

            var encodedJwt =
                _jwtGenerator.GenerateJwt(
                        registeredUser.Id,
                        registeredUser.UserName!,
                        registeredUser.Email!
                    );

            var authResult = new AuthResult(
                    registeredUser.Id,
                    encodedJwt
                );

            return authResult;
        }
    }
}
