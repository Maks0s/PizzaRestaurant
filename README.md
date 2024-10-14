# Monolith CRUD+Auth API based on pizza restaurant theme

## Used technologies:
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

## Architecture:
The application structure follows clean architecture principles:
- Domain layer: provides 'Pizza' and 'AuthUser' domain entities
- Application layer: contains application interfaces, expected errors, PipelineBehavior handlers and logic related to CQRS
- Infrastructure layer: responsible for working with the database and external services.
- Presentation layer: collects the configuration of the whole application and provides communication with the client via API and related elements
  
Each layer contains its own DependencyInjection file with registration and configuration of all related services.

Also, app has a separate folder with unit tests and integration tests that check the entire path of the request: from receiving it, to saving it to the database. Thanks to Docker and the TestContainers library, the tests are run independently, but in a full working environment.

The root of the source code contains Dockerfile.
 
The root of the application contains Docker-compose.yaml and related .env files for configuration purposes
 - All connection strings, environment variables, logins and passwords have not been hidden and have been put into Git for easy repository startup and verification

## API request examples and results
### Endpoints
![image](https://github.com/Maks0s/PizzaRestaurant/assets/89703990/94e65b5a-c81a-433b-9cf8-4bdd18f2d09e)

### Schemas
![image](https://github.com/Maks0s/PizzaRestaurant/assets/89703990/192f4e43-26c8-4996-a281-039e12d1b780)

### /auth/register
- Data for valid registration
  ```json
  {
     "username": "test",
     "email": "test@gmail.com",
     "password": "StrongPass1!"
  }
  ```
- Successful registration
  ![image](https://github.com/user-attachments/assets/3e46bb45-103c-427a-a56d-d0bc029cdfc0)
- Data for invalid registration
  ```json
  {
     "username": "a",
     "email": "test",
     "password": "!"
  }
  ```
- Registration error
  ![image](https://github.com/user-attachments/assets/ddc092f6-aff6-4ece-8eaf-2d38ef2f9795)

### /auth/login
- Data for valid login
  ```json
  {
     "username": "test",
     "email": "test@gmail.com",
     "password": "StrongPass1!"
  }
  ```
- Successful login
  ![image](https://github.com/user-attachments/assets/bb343e50-c2a3-4fdb-8237-5898b637ae83)
- Data for invalid login
  ```json
  {
     "username": "test",
     "email": "test@gmail.com",
     "password": "!"
  }
  ```
- Login error
  ![image](https://github.com/user-attachments/assets/85ab0379-7821-467e-8215-475e57d7fdde)

### /pizza/add
- Data for valid Pizza creation
  ```json
   {
     "name": "testPizza",
     "crustType": "Thin",
     "ingredients": "TestTestTestTestTest",
     "price": 13
   }
  ```
- Successful creation
  ![image](https://github.com/user-attachments/assets/48460cf8-8486-471b-87a7-46ee0278cb09)
- Data for invalid Pizza creation
  ```json
   {
     "name": "",
     "crustType": "",
     "ingredients": "",
     "price": -1
   }
  ```
- Creation error
  ![image](https://github.com/user-attachments/assets/aed67b44-ca8a-4c34-b2a5-471a7ee33eb9)


### /pizza/{id}
- Data of existing Pizza
  ```json
   {
     "id": "d61ae10e-383d-4f47-a619-29f6d9a07182"
   }
  ```
- Successful receiving Pizza
  ![image](https://github.com/user-attachments/assets/d571850a-13ea-4695-ae4f-9bf4c66c5ee6)
- Data of non-existing Pizza
  ```json
   {
     "id": "ea387db7-4051-4dc7-a889-1c65d672eaac"
   }
  ```
- Receiving error
  ![image](https://github.com/user-attachments/assets/cd4eebdf-b6c0-44eb-a0dd-f4aeadea7b59)


### /pizza/all
- Successfully receiving data from a populated database
  ![image](https://github.com/user-attachments/assets/c0aafa8e-accb-4656-aaf6-a5cd735497f7)
- Receiving error
  ![image](https://github.com/user-attachments/assets/f0c17ece-31ef-4cf0-83cf-f1d0271644a5)

### /pizza/update/{id}
- Data for valid Pizza update
  ```json
   {
     "id": "d61ae10e-383d-4f47-a619-29f6d9a07182"
   }
  ```
  ```json
   {
     "name": "UpdatedPizza",
     "crustType": "Tick",
     "ingredients": "Updated Ingredients!",
     "price": 21
   }
  ```
- Successful update
  ![image](https://github.com/user-attachments/assets/70ef0230-9e1f-4e47-b605-5aebd0cbba04)
- Data for invalid Pizza update
  ```json
   {
     "id": "ea387db7-4051-4dc7-a889-1c65d672eaac"
   }
  ```
  ```json
   {
     "name": "UpdatedPizza",
     "crustType": "Tick",
     "ingredients": "Updated Ingredients!",
     "price": 21
   }
  ```
- Update error
  ![image](https://github.com/user-attachments/assets/e0f0036a-2cad-4c23-9b01-7ff232051965)

### /pizza/delete/{id}
- Data for deleting the existing Pizza
  ```json
   {
     "id": "d61ae10e-383d-4f47-a619-29f6d9a07182"
   }
  ```
- Successful delete
  ![image](https://github.com/user-attachments/assets/31d43b68-68bb-4754-a75f-833d1f9c42a4)
- Data for deleting a non-existing Pizza
  ```json
   {
     "id": "ea387db7-4051-4dc7-a889-1c65d672eaac"
   }
  ```
- Delete error
  ![image](https://github.com/user-attachments/assets/893cc5c2-79be-4a88-ab30-d828a163c201)


## Instructions for launching the project
### Local launch and launching external services (MsSQL Server) on Docker
To do this, you need to fully clone the repository, then, being in the root folder of the application, run the console command:
``` Shell
docker-compose -f Docker-compose.development.yaml up -d
```
This will launch the Docker-compose.development.yaml file, which contains only the external services configurations.
After using the command, it is recommended to wait a bit so that the external services have time to spin up and the internal ones don't get an error when connecting to unconfigured services.

Next, run PizzaRestaurant.sln in the compiler.

Send requests to app directly or use SwaggerUI at the link: https://localhost:7065/swagger/index.html
And view the results in the same SwaggerUI or in the PizzaRestaurant.sln console

### Running the entire application on Docker
To do this, you only need to have:
- Docker-compose.yaml

And the associated .env environment variable files
- apiconfig.env
- sapassword.env
- sqlconfig.env

Also, for correct https connection of ASP.Net Core application, we need to configure the certificate for development purposes.
If the certificate is already exists and we know the password for it, we simply insert the password into a variable in the apiconfig.env file:
```d
ASPNETCORE_Kestrel__Certificates__Default__Password=[YourPass]
```
If we don't remember the password, we can create it with a couple of console commands:

Delete the current certificates:
```shell
dotnet dev-certs https --clean
```

Create a new certificate and insert the password already used in apiconfig.env: ASPNETCOREKestrelCertificatesDefault
```shell
dotnet dev-certs https -ep “$env:USERPROFILE\.aspnet\https\aspnetapp.pfx” -p ASPNETCOREKestrelCertificatesDefault
```

Specify that this certificate can be trusted
```shell
dotnet dev-certs https --trust
```

And to launch the application, being in the root folder, run the console command:
``` Shell
docker-compose -f Docker-compose.yaml up -d
```

Requests can be sent directly:
- http://localhost:5000/pizza
- https://localhost:5001/pizza
- http://localhost:5000/auth
- https://localhost:5001/auth

- Or using the SwaggerUI at: 
https://localhost:5001/swagger/index.html

See the results in the same SwaggerUI or in the logs of containers running on Docker
