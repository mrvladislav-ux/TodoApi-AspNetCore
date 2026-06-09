# TodoApi-AspNetCore

Simple REST API for task management built with ASP.NET Core.

## Technologies

* ASP.NET Core
* Entity Framework Core
* SQLite
* JWT Authentication

## Features

* User registration
* User login
* JWT authentication
* Password hashing
* Create tasks
* Update tasks
* Delete tasks
* Search tasks
* Filter completed tasks
* Filter pending tasks
* User-specific task ownership

## Database

Uses SQLite with Entity Framework Core migrations.

## Testing

You can test API using:

* Swagger UI
* Postman


## Run

```bash
dotnet restore
dotnet build
dotnet run
```

Swagger:

```
http://localhost:5018/swagger
```

## Roadmap

* Pagination
* PostgreSQL support
* Docker support
