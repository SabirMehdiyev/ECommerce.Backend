# E-Commerce REST API

A RESTful E-Commerce API built with **ASP.NET Core 8**, **Entity Framework Core**, and **SQL Server**, following a layered Clean Architecture approach.

The project implements core e-commerce backend functionality, including product and category management, JWT-based authentication, role-based authorization, and user-specific shopping carts.

## Tech Stack

- **Framework:** ASP.NET Core 8 Web API
- **Language:** C#
- **Database:** Microsoft SQL Server
- **ORM:** Entity Framework Core
- **Authentication:** ASP.NET Core Identity, JWT Bearer
- **Authorization:** Role-based authorization (Admin / Customer)
- **Validation:** FluentValidation
- **Object Mapping:** AutoMapper
- **API Documentation:** Swagger / OpenAPI
- **Architecture:** Clean Architecture, Repository Pattern, Dependency Injection

## Architecture

The solution is organized into four layers:

```text
ECommerce.Domain
    Entities, base models, and domain constants

ECommerce.Application
    DTOs, interfaces, services, validators, and mappings

ECommerce.Infrastructure
    EF Core, database context, repositories, Identity, and JWT services

Ecommerce.API
    Controllers, middleware, and application configuration
```

This separation keeps API endpoints, business logic, and data access responsibilities organized and maintainable.

## Features

### Product Management
- Create, read, update, and delete products
- Product filtering, sorting, and pagination
- Category association and validation
- Optional product image URL support
- Stock and price validation

### Category Management
- Full CRUD operations
- Product–Category relationships
- Prevention of category deletion when related products exist

### Authentication & Authorization
- User registration and login using ASP.NET Core Identity
- Password hashing and account lockout
- JWT access token generation and validation
- Admin and Customer roles
- Admin-only product and category modifications
- Transaction-based user registration and role assignment

### Shopping Cart
- Add products to a personal cart
- View, update, and remove cart items
- Clear the shopping cart
- Quantity and stock validation
- User-specific cart access based on JWT identity

### Validation & Error Handling
- Request validation using FluentValidation
- Centralized exception handling middleware
- Standardized error responses using ProblemDetails
- Appropriate HTTP status codes

## API Overview

| Module | Endpoints | Access |
|---|---|---|
| Authentication | Register, Login | Public |
| Products | GET products, GET by ID | Public |
| Products | POST, PUT, DELETE | Admin |
| Categories | GET categories, GET by ID | Public |
| Categories | POST, PUT, DELETE | Admin |
| Cart | View, add, update, remove, clear | Authenticated users |

Shopping cart operations are restricted to the authenticated user's own cart.

## Getting Started

### Prerequisites

- .NET 8 SDK
- Microsoft SQL Server
- Visual Studio 2022 or another .NET-compatible IDE

### 1. Clone the Repository

```bash
git clone https://github.com/SabirMehdiyev/ECommerce.Backend.git
cd ECommerce.Backend
```

### 2. Configure Application Secrets

The project uses .NET User Secrets for local development.

In Visual Studio, right-click the **Ecommerce.API** project and select **Manage User Secrets**.

Configure the following values:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "<sql-server-connection-string>"
  },
  "JwtSettings": {
    "Key": "<base64-encoded-secret-key>",
    "Issuer": "<issuer>",
    "Audience": "<audience>",
    "ExpirationMinutes": 30
  },
  "AdminAccount": {
    "Email": "<development-admin-email>",
    "Password": "<development-admin-password>"
  }
}
```

Use a secure randomly generated JWT signing key of at least 256 bits.

Keep credentials and secrets outside source control.

### 3. Apply Database Migrations

From the solution directory, run:

```bash
dotnet ef database update --project ECommerce.Infrastructure --startup-project Ecommerce.API
```

The command requires the EF Core CLI tools and an accessible SQL Server instance.

### 4. Run the Application

```bash
dotnet restore
dotnet run --project Ecommerce.API
```

Swagger UI is available at `/swagger` when running in the Development environment.

### 5. Authentication

Register a Customer account through the registration endpoint, then log in to receive a JWT access token.

Use the **Authorize** button in Swagger to authenticate requests to protected endpoints.

In Development, the application seeds roles and a development Admin account using the configured credentials.


## Development Notes

- Entity Framework Core migrations are used to manage database schema changes.
- Application secrets are kept outside the repository.
- Core API functionality has been manually tested using Swagger.
- Automated test coverage and additional production-focused improvements may be introduced in future updates.

