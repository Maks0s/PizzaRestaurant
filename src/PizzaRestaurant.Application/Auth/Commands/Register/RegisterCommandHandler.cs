using ErrorOr;
using Microsoft.AspNetCore.Identity;
using PizzaRestaurant.Application.Auth.Common.AuthDto;
using PizzaRestaurant.Application.Common.AppErrors;
using PizzaRestaurant.Application.Common.Interfaces.Authentication;
using PizzaRestaurant.Application.Common.Interfaces.CQRS;
using PizzaRestaurant.Domain.Entities;

namespace PizzaRestaurant.Application.Auth.Commands.Register
{
    public class RegisterCommandHandler
        : ICommandHandler<RegisterCommand, AuthResult>
    {
        private readonly UserManager<AuthUser> _userManager;
        private readonly IJwtGenerator _jwtGenerator;

        public RegisterCommandHandler(
                UserManager<AuthUser> userManager,
                IJwtGenerator jwtGenerator
            )
        {
            _userManager = userManager;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<ErrorOr<AuthResult>> Handle(
                RegisterCommand command,
                CancellationToken cancellationToken
            )
        {
            var registeredUser =
                await _userManager
                    .FindByEmailAsync(command.Email);

            if(registeredUser is not null)
            {
                return Errors.Auth.UserAlreadyExists(command.Email);
            }

            registeredUser = new AuthUser()
            {
                UserName = command.Username,
                Email = command.Email
            };


            var registerResult =
                await _userManager
                    .CreateAsync(
                        registeredUser,
                        command.Password
                    );

            if(!registerResult.Succeeded)
            {
                return Errors.ServerDataManipulation.NotAdded();
            }

            var encodedJwt =
                _jwtGenerator.GenerateJwt(
                        registeredUser.Id,
                        registeredUser.UserName,
                        registeredUser.Email
                    );

            var authResult = new AuthResult(
                    registeredUser.Id,
                    encodedJwt
                );

            return authResult;
        }
    }
}
