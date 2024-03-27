using ErrorOr;
using System.Net;

namespace PizzaRestaurant.Application.Common.AppErrors
{
    public static partial class Errors
    {
        public static class ServerDataManipulation
        {
            public static Error NotAdded() =>
                Error.Custom(
                        (int)HttpStatusCode.InternalServerError,
                        "Your data has not been added to the server",
                        "Something happened while adding your data to the server. Please try again later."
                    );

            public static Error NotReceived() =>
                Error.Custom(
                        (int)HttpStatusCode.InternalServerError,
                        "Requested data was not received",
                        "Something happened when requesting data from the server. Please try again later."
                    );
        }
    }
}
