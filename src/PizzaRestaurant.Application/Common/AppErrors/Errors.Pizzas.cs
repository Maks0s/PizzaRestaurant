using ErrorOr;
using System.Net;

namespace PizzaRestaurant.Application.Common.AppErrors
{
    public static partial class Errors
    {
        public static class Pizzas
        {
            public static Error NotFound(Guid id) =>
                Error.Custom(
                        (int)HttpStatusCode.NotFound,
                        "Requested pizza was not found",
                        $"Requested pizza with ID: {id} was not found. Please correct your request."
                    );
        }
    }
}
