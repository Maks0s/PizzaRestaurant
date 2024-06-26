﻿using ErrorOr;
using System.Net;

namespace PizzaRestaurant.Application.Common.AppErrors
{
    public static partial class Errors
    {
        public static class Auth
        {
            public static Error UserAlreadyExists(string email) =>
                Error.Custom(
                        (int)HttpStatusCode.Conflict,
                        "User already registered",
                        $"User with email: {email} already exists"
                    );

            public static Error InvalidCredentials() =>
                Error.Custom(
                        (int)HttpStatusCode.Unauthorized,
                        "Invalid credentials",
                        "Email or password is incorrect"
                    );
        }
    }
}
