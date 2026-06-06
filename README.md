# TodoApi-AspNetCore

Simple REST API for task management built with ASP.NET Core.

## Technologies

* ASP.NET Core
* Entity Framework Core
* SQLite
* Swagger
* Git

## Features

* Create task
* Get all tasks
* Get task by ID
* Update task
* Delete task
* Search tasks by title
* Filter completed tasks
* Filter pending tasks

## Database

Uses SQLite with Entity Framework Core migrations.

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

* JWT Authentication
* User accounts
* Pagination
* PostgreSQL support
* Docker
