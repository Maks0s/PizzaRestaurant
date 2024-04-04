using Bogus;
using System.Runtime.Serialization;

namespace PizzaRestaurant.Infrastructure.UnitTests.TestUtils
{
    public static class BogusExtensions
    {
        public static Faker<T> WithRecord<T>(this Faker<T> faker) where T : class
        {
            faker.CustomInstantiator(_ =>
                FormatterServices.GetUninitializedObject(typeof(T)) as T
                );
            return faker;
        }
    }
}
