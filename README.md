# CRUD API based on pizza restaurant theme
### ASP.NET Core web API project with features of:
- Clean architecture
- CQRS
- All default CRUD operations
- Database integration
- Docker and docker-compose support
- JWT authentication
- Integration and Unit testing
- Custom error handling
- Model validation
- Logging

### Used technologies:
- ASP.Net Core (Web API framework)
- MediatR (CQRS/Logging&Validation pipeline behavior)
- Entity framework Core (ORM)
- MS SQL Server (DB)
- Identity framework (Auth)
- Xunit (Tests)
- ErrorOr (Error handling)
- Fluent Validation (Model validation)
- Serilog (Logging)
- Mapperly (Mapping)
- Docker Desktop

### Endpoints
![image](https://github.com/Maks0s/PizzaRestaurant/assets/89703990/94e65b5a-c81a-433b-9cf8-4bdd18f2d09e)


### Schemas
![image](https://github.com/Maks0s/PizzaRestaurant/assets/89703990/192f4e43-26c8-4996-a281-039e12d1b780)


### Clean architecture separation:
- Domain layer includes:
   - Pizza entity on which all standard CRUD operations are performed;
   - AuthUser entity which is used for identity
- Application layer includes:
   - All CQRS stuff related to entities;
   - All expected errors: Auth, Pizzas, Data manipulation, and Validation;
   - Behaviors for general logging and model validation;
   - Interfaces of services;
- Infrastructure layer includes:
   - All persistence related stuff:
      - Repository
      - DbContexts
      - Migrations
   - Implementation of JWT generation and configuration
- Presentation layer inсludes:
   - DTO's;
   - Mapping logic;
   - Controllers:
      - Errors controller for unexpected problems
      - Base controller for overridden logic
      - Entities controllers for CRUD and auth

Each layer contains its own DependencyInjection file with registration and configuration of all related services.
