using Bogus;
using PizzaRestaurant.Infrastructure.UnitTests.TestUtils;

namespace PizzaRestaurant.Infrastructure.UnitTests.Authentication.TestUtils
{
    public class AuthUserGenerator
    {
        private readonly Faker<AuthUserDto> _authUserGenerator =
            new Faker<AuthUserDto>()
                .WithRecord()
                .RuleFor(dto => dto.UserId, Guid.NewGuid().ToString())
                .RuleFor(dto => dto.Username, f => f.Person.UserName)
                .RuleFor(dto => dto.Email, f => f.Person.Email);

        public AuthUserDto GenerateAuthUserDto()
        {
            return _authUserGenerator.Generate();
        }
    }
}
