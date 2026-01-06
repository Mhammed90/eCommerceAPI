# E-Commerce Solution

An ASP.NET Core Web API for an e-commerce platform, built with clean architecture principles.

## Overview

This project implements a full-featured e-commerce backend API with user authentication, product management, shopping cart, and payment processing capabilities. It follows a layered architecture separating concerns into Domain, Application, Infrastructure, and Presentation layers.

## Architecture

The solution is structured using Clean Architecture:

- **eCommerce.Domain**: Contains entities, interfaces, and business logic.
- **eCommerce.Application**: Application services, DTOs, validation, and mapping.
- **eCommerce.Infrastructure**: Data access, repositories, migrations, and external services.
- **eCommerce.Presentation**: ASP.NET Core Web API controllers and configuration.

## Features

- **Authentication & Authorization**: JWT-based authentication with refresh tokens, role-based access (Admin/User)
- **Product Management**: CRUD operations for products and categories
- **Shopping Cart**: Add/remove items, manage cart contents
- **Payment Integration**: Support for multiple payment methods including Stripe
- **Logging**: Structured logging with Serilog
- **API Documentation**: Swagger/OpenAPI documentation
- **Database**: SQL Server with Entity Framework Core

## Technologies Used

- **Framework**: ASP.NET Core 9.0
- **Language**: C# 12
- **Database**: SQL Server
- **ORM**: Entity Framework Core
- **Authentication**: JWT Bearer Tokens
- **Logging**: Serilog
- **API Documentation**: Swagger/OpenAPI
- **Payment**: Stripe
- **Validation**: FluentValidation
- **Mapping**: AutoMapper

## Prerequisites

- .NET 9.0 SDK (version 9.0.110 or later)
- SQL Server (LocalDB or full instance)
- Visual Studio 2022 or Rider IDE

## Setup Instructions

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd ecommerceSolution
   ```

2. **Restore packages**
   ```bash
   dotnet restore
   ```

3. **Database Setup**
   - Update the connection string in `eCommerce.Presentation/appsettings.json` if needed
   - Run migrations to create the database:
   ```bash
   dotnet ef database update --project eCommerce.Infrastructure --startup-project eCommerce.Presentation
   ```

4. **Configure Stripe** (optional)
   - Add your Stripe secret key to `appsettings.json` under the "Stripe" section

5. **Run the application**
   ```bash
   dotnet run --project eCommerce.Presentation
   ```

The API will be available at `https://localhost:5000` with Swagger UI at `https://localhost:5000/swagger`.

## API Endpoints

### Authentication
- `POST /api/Authentication/login` - User login
- `POST /api/Authentication/register` - User registration
- `POST /api/Authentication/refresh-token` - Refresh JWT token

### Products
- `GET /api/Product` - Get all products
- `GET /api/Product/{id}` - Get product by ID
- `POST /api/Product` - Create product (Admin)
- `PUT /api/Product/{id}` - Update product (Admin)
- `DELETE /api/Product/{id}` - Delete product (Admin)

### Categories
- `GET /api/Category` - Get all categories
- `GET /api/Category/{id}` - Get category by ID
- `POST /api/Category` - Create category (Admin)
- `PUT /api/Category/{id}` - Update category (Admin)
- `DELETE /api/Category/{id}` - Delete category (Admin)

### Cart
- `GET /api/Cart` - Get user's cart
- `POST /api/Cart` - Add item to cart
- `PUT /api/Cart/{id}` - Update cart item
- `DELETE /api/Cart/{id}` - Remove item from cart

### Payment
- `GET /api/Payment/methods` - Get available payment methods
- `POST /api/Payment/checkout` - Process payment

## Database Schema

The application uses the following main entities:

- **Users**: Extended IdentityUser with full name
- **Products**: Items for sale with name, price, description, image, quantity
- **Categories**: Product categories
- **Cart Items**: User's shopping cart contents
- **Payment Methods**: Available payment options
- **Refresh Tokens**: For JWT token refresh

## Configuration

Key configuration files:
- `appsettings.json`: Application settings, connection strings, JWT config
- `global.json`: .NET SDK version specification
- `eCommerce.Presentation.csproj`: Project dependencies



## Security

- JWT authentication with configurable expiration
- Role-based authorization (Admin/User roles)
- CORS configured for specific origins
- Input validation using FluentValidation
- Secure password hashing via ASP.NET Core Identity

## Logging

Logs are written to:
- Console output
- Daily rolling log files in `log/log.txt`

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests if applicable
5. Submit a pull request

## License

This project is licensed under the MIT License.
