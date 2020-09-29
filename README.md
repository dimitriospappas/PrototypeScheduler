# PrototypeScheduler

ASP.NET Core web API using EF Core and Swagger, simulating a prototype scheduling application backend.

## Deployment

Deployment has been organised using Docker; the application runs on one, the database (MS SQL Server) on another.

#### Dockers

- In the `docker-compose.yml` file the settings can be changed for the database password and port bindings in general, and in the `docker-compose.override.yml` the ones used for development environment.
- The application should be started using **development** environment.
  {Scheme}://localhost:{ServicePort}/swagger/index.html

#### Database

- The database Docker container is named `schedulerdb` and is visible to the API through that alias. To ensure that the API runs successfully once a new Docker image of the SQL server is pulled, it must be running and the user should connect to it (e.g., Docker CLI, MSSQLS Management Studio) and create a new database named `scheduler`.
- When the API application starts, it performs the initial EF migration to create the tables, then it populates it with some test data through the static class that can be reviewed at `InitialSeed.cs`.
- Currently, the solution to wait for the database to be ready before starting the application with Docker does not seem to work properly and could not be troubleshooted in time, therefore it might be required to restart the application as needed so that it connects to a running database ready to accept requests.

#### Replacing the Database

To replace the database with another one for testing, the following steps should be taken:

1. Remove the `schedulerdb` dependency of the application in `docker-compose.yml`.
2. Remove the `schedulerdb` entry from the `docker-compose.yml` file.
3. In the `SchedulerAPI/application.settings.json` file, replace the value for `SchedulerDB` under the `ConnectionStrings` property with the settings reflecting the new desired SQL Server.

#### API

- The `SchedulerAPI` application should be run with a _development_ profile.
- It is suggested to use _Docker_ debug settings.
- Once the application starts, it will open Swagger in the local browser:

```bash
# where Scheme: HTTP(S) and {ServicePort} the port bound in settings
$ {Scheme}://localhost:{ServicePort}/swagger/index.html
```

## Implementation

Implementation of scheduling project backend functionality has been completed as follows:

> Infrastructure Persistence Layer

- [x] Models through EF Core to reflect the domain.
- [x] Code first approach to design and seeding of the database.
- [ ] Infrastructure and Persistence Ignorance

> API Functionality

- [x] CRUD operations that reflect a domain-driven design.
- [x] External testing applications used to check functionality.
- [x] Web UI application interfacing correctly. (for tested functions)
- [ ] Clear documentation available to API consumers. (Swagger)

> Extra Features

- [x] Swagger integration for testing purposes.
- [ ] Secure and enhance API interactions with DTOs.
- [ ] Implementation of integration with external system.

## Limitations

- Until unit testing is performed, testing results are empirical and anecdotal
- New entity creation date approach with annotations as Identity field was not performing correctly for SQL Server and troubleshooting was scrapped due to time constraints (should require custom SQL code at initial migration to handle such functions).

## Future Improvements

- Implement missing functionality. (only creating multiple skills)
- Apply Repository pattern to abstract the persistence layer.
- Extensive unit testing with Persistence ignorance thanks to dependency injection.
- Remove annotations (ambiguous, unreliable in certain cases) and define additional entity requirements and relationships through Fluent API in a code-first, abstract manner.
- Over-posting prevention and using Data Transfer Objects to enhance API functionality.
- Use DDD principlies in designing a solution to integration with an external system.
