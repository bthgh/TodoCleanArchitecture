# TodoCleanArchitecture

This repository provides a .NET Clean Architecture template designed to help developers get started with building clean, maintainable, and scalable applications. It follows best practices for structuring code in a way that ensures separation of concerns, ease of testing, and scalability.

## Features

- .NET 9
- Implements Clean Architecture principles
- Domain-Driven Design (DDD) approach
- Domain Events
- Dependency Injection (DI) configuration
- CQRS (Command Query Responsibility Segregation) - MediatR
- Supports Entity Framework Core (EF Core) for data persistence
- Unit Of Work 
- Logging with Serilog
- Identity & Security (JWT)
- Repository Pattern


## Folder Structure
![Clean Architecture](https://raw.githubusercontent.com/bthgh/TodoCleanArchitecture/28e5890703dd824c1736a9e66ee00b515b39204c/CleanArchitectureDiagram.png)

The folder structure adheres to the Clean Architecture principles, dividing the application into distinct layers:

- **Api**: The API layer that handles HTTP requests and responses.
- **Application**: The layer containing business logic, services, and use cases.
- **Domain**: The core domain model and entities that define the business.
- **Infrastructure**: The implementation of external dependencies such as database, file storage, etc.

## Getting Started

To get started with this template, follow these steps:

### Prerequisites

Make sure you have the following installed:

- [.NET 9 or later](https://dotnet.microsoft.com/download/dotnet)
- [Visual Studio 2022 or later](https://visualstudio.microsoft.com/) or [JetBrains Rider](https://www.jetbrains.com/rider/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) 

### Clone the Repository

```bash
git clone https://github.com/bthgh/TodoCleanArchitecture.git
cd TodoCleanArchitecture
```

### How to add migrations

```
dotnet ef migrations add InitialCreate --startup-project ../TodoCleanArchitecture.Api --output-dir Persistence/Data/Migrations
```

### How to update the database

```
dotnet ef database update --startup-project ../TodoCleanArchitecture.Api  
```

