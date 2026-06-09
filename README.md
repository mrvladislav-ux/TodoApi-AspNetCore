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

## API Screenshots

Swagger UI:

<img width="1441" height="745" alt="Swagger Screenshot" src="https://github.com/user-attachments/assets/592f14f3-0964-45eb-abb4-b973f0e5443d" />

Postman: 

<img width="839" height="741" alt="Postman Screenshot" src="https://github.com/user-attachments/assets/c5b879c3-aa60-4525-8ac7-0809996c7388" />


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
