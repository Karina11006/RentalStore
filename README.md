# Sports Equipment Rental Application

The **Sports Equipment Rental** application allows users to rent various types of sports equipment online. This project was built using **ASP.NET Core** and is designed for sports rental shops looking to expand their services online.

## Introduction

The goal of this project is to provide a user-friendly platform for customers to easily and quickly rent sports equipment. The application features an intuitive interface and offers functionality for both customers and store administrators.

## Features

### User Features:
- **Browse Equipment**: Users can explore the available sports equipment inventory.
- **Make Reservations**: Users can reserve equipment for a specified period.
- **Cart and Checkout**: Users can add items to their cart and proceed with reservations.
### Admin Features:
- **Manage Equipment**: Administrators can add, edit, and delete equipment in the inventory.
- **Handle Reservations**: Administrators can view and manage all active reservations.
- **Manage Categories**: Administrators can add, edit, and delete categories.

  
### Additional Features:
- **Sorting and Filtering**: Users can sort and filter the available equipment by category, price, and quantity.
- **Responsive Design**: The application is optimized for various devices.

## Technologies Used

- **C#**
- **ASP.NET Core** (for backend and API services)
- **Entity Framework Core** (for database interaction)
- **Blazor Server** and **Blazor WebAssembly** (for building the client-side application and the admin panel)

## Architecture

The project follows **Clean Architecture** principles, ensuring separation of concerns, maintainability, and scalability. This design provides flexibility for future expansion or modification.

## Project Structure

```text
RentalStore.Application/          # Contains business logic and services layer
RentalStore.BlazorClientv2/       # The Blazor WebAssembly project for the user interface
RentalStore.BlazorServer/         # The Blazor Server project for the admin interface
RentalStore.Domain/               # Contains domain entities and business logic
RentalStore.Infrastructure/       # Handles database operations with Entity Framework Core
RentalStore.SharedKernel/         # Contains shared classes and utilities
RentalStore.WebAPI/               # Provides RESTful API services using ASP.NET Core
```
### Equipment Management:
![adminpanel](https://github.com/user-attachments/assets/3d0e3f74-adb6-4384-956f-61195034c269)

### Shopping cart
![shoppingcart](https://github.com/user-attachments/assets/07cd4d07-1ec5-440e-88bc-856ad7a36909)

### Client View (Sports Collection):
![user_collections](https://github.com/user-attachments/assets/5a18dd52-8380-40c5-b767-b9023cdfa0ac)

## API Documentation

The **API** is documented using **Swagger**, providing an interactive interface for testing API endpoints.
![swagger](https://github.com/user-attachments/assets/e0400a3a-ef6b-4563-abbc-09c479ada3ff)

### Prerequisites:

1. **.NET SDK** (version 5.0 or later)
2. **Visual Studio or Visual Studio Code**
   - Open the solution and go to `Tools/Options/Nuget Package Manager` and set the **Package Source** to: `https://api.nuget.org/v3/index.json`.
3. **Git** (for cloning the repository)

4. ### Running the Application


1. **Install required packages**:

   - Open the solution and go to `Tools/Options/Nuget Package Manager`.
   - Set the **Package Source** to: `https://api.nuget.org/v3/index.json`.

2. **Add the following Nuget packages** (for **.NET 6.0**, do not use versions higher than **7.0.16**):
   - `Microsoft.EntityFrameworkCore`
   - `Microsoft.EntityFrameworkCore.Sqlite`

3. **Download a tool** for viewing the contents of **SQLite** databases, e.g., **DB Browser**.

## Authors
* Karina Krotkiewicz (karina.krotkiewicz@gmail.com)
* Karol Kita (kkita970@gmail.com)
* Konrad Folwarski
* Mariusz Dyrla
