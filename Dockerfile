FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/PizzaRestaurant.Domain/PizzaRestaurant.Domain.csproj", "PizzaRestaurant.Domain/"]
COPY ["src/PizzaRestaurant.Application/PizzaRestaurant.Application.csproj", "PizzaRestaurant.Application/"]
COPY ["src/PizzaRestaurant.Infrastructure/PizzaRestaurant.Infrastructure.csproj", "PizzaRestaurant.Infrastructure/"]
COPY ["src/PizzaRestaurant.Presentation/PizzaRestaurant.Presentation.csproj", "PizzaRestaurant.Presentation/"]
RUN dotnet restore "PizzaRestaurant.Presentation/PizzaRestaurant.Presentation.csproj"
COPY . .
WORKDIR "src/PizzaRestaurant.Presentation"
RUN dotnet build "PizzaRestaurant.Presentation.csproj"  -c Realese -o /app/build

FROM build AS publish
RUN dotnet publish "PizzaRestaurant.Presentation.csproj" -c Realese -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PizzaRestaurant.Presentation.dll"]