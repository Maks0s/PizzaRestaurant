using Bogus;
using System.Runtime.Serialization;

namespace PizzaRestaurant.IntegrationTests.TestUtils
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
