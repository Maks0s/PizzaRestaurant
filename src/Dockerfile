FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/PizzaRestaurant.Presentation/PizzaRestaurant.Presentation.csproj", "src/PizzaRestaurant.Presentation/"]
COPY ["src/PizzaRestaurant.Application/PizzaRestaurant.Application.csproj", "src/PizzaRestaurant.Application/"]
COPY ["src/PizzaRestaurant.Domain/PizzaRestaurant.Domain.csproj", "src/PizzaRestaurant.Domain/"]
COPY ["src/PizzaRestaurant.Infrastructure/PizzaRestaurant.Infrastructure.csproj", "src/PizzaRestaurant.Infrastructure/"]
RUN dotnet restore "./src/PizzaRestaurant.Presentation/PizzaRestaurant.Presentation.csproj"
COPY . .
WORKDIR "/src/src/PizzaRestaurant.Presentation"
RUN dotnet build "./PizzaRestaurant.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PizzaRestaurant.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PizzaRestaurant.Presentation.dll"]