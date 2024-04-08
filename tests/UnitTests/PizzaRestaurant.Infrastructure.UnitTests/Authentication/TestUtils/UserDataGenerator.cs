using Bogus;
using PizzaRestaurant.Infrastructure.UnitTests.TestUtils;

namespace PizzaRestaurant.Infrastructure.UnitTests.Authentication.TestUtils
{
    public class UserDataGenerator
    {
        private readonly Faker<UserDataDto> _authUserGenerator =
            new Faker<UserDataDto>()
                .WithRecord()
                .RuleFor(dto => dto.UserId, Guid.NewGuid().ToString())
                .RuleFor(dto => dto.Username, f => f.Person.UserName)
                .RuleFor(dto => dto.Email, f => f.Person.Email);

        public UserDataDto GenerateAuthUserDto()
        {
            return _authUserGenerator.Generate();
        }
    }
}
