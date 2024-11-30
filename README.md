Exchange Data in Secure Manner - ASP.NET Core Web API with Clean Architecture

This project demonstrates an ASP.NET Core Web API with clean architecture principles, using JWT Authentication for secure data exchange. It includes features like user registration, login, and managing employee details.

Project Structure

The project is organized using clean architecture:

Application Layer: Contains interfaces and DTOs (Data Transfer Objects).
Domain Layer : Holds the core entities/models of the project.
Infrastructure Layer: Handles database interactions using Entity Framework Core.
Web API Layer: Manages the REST API controllers for handling client requests.
Web Assembly Layer: Manages the user interface for registration, login, and viewing employee details.
Prerequisites

Make sure you have the following installed:

.NET 9 SDK
SQL Server
Visual Studio 2022
Setup Guide

Clone the Repository
git clone https://github.com/yourusername/secure-data-exchange.git
cd secure-data-exchange
Database Configuration Open appsettings.json and update the connection string with your SQL Server details:

"ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=YOUR_DATABASE_NAME;User ID=YOUR_USER_ID;Password=YOUR_PASSWORD;"
}
Ensure your SQL Server is running.
Run Migrations

Open the Package Manager Console in Visual Studio.
Run the following command to create the database:
Update-Database
Build and Run the Application

Open the solution in Visual Studio.
Set the Web API project as the startup project.
Press F5 or run the application using:
dotnet run
The API and Swagger documentation should now be accessible at https://localhost:7260/swagger or http://localhost:7260/swagger.
Test API Endpoints with Swagger

Open your browser and navigate to https://localhost:7260/swagger or http://localhost:7260/swagger.
Swagger provides a UI to test all available endpoints:
Registration: POST /api/auth/register
Login: POST /api/auth/login
Get Employee Details: GET /api/employee (JWT Token Required)
Project Usage

User Registration and Login

The application provides registration and login functionality.
JWT Authentication is used to secure routes.
Upon successful login, a JWT token is generated, which must be included in the header for accessing secure endpoints.


POST /api/auth/register

Registers a new user.
Request Body:
{
  "username": "example",
  "password": "example123",
  "email": "example@example.com"
}
POST /api/auth/login

Logs in a user and returns a JWT token.
Request Body:
{
  "username": "example",
  "password": "example123"
}


GET /api/Details
Retrieves a list of Details. Requires a valid JWT token in the Authorization header.
Header:
Authorization: Bearer YOUR_JWT_TOKEN
Technologies Used

ASP.NET Core 9.0
Entity Framework Core
SQL Server
JWT Authentication
Clean Architecture Principles
Blazor WebAssembly
