## InventoryMaster 360
A robust, N-Tier Architecture based Product Management System built with ASP.NET Core MVC. This project simulates a real-world back-office application, focusing on strict business logic, data integrity, and clean code principles.


## Architecture
The project follows a strict N-Tier Architecture to ensure separation of concerns:

Core Layer: Entities (Product, Category) and Interfaces.

Data Access Layer (DAL): Entity Framework Core context, Migrations, and Concrete Repositories.

Business/Web Layer: Controllers, FluentValidation rules, and Views.

## Overview
InventoryMaster 360 is designed to manage product lifecycles with high precision. Unlike standard CRUD applications, this system enforces strict validation rules to prevent data corruption and logical errors (e.g., selling below cost). It utilizes advanced Entity Framework Core features like Global Query Filters for soft deletes and Interceptors for automatic auditing.

## Features
Generic Repository Pattern: A centralized data access logic (GetAll, GetById, Add, Update, Delete) applied to all entities.

Dependency Injection (DI): Loosely coupled architecture where Repositories and Services are injected into Controllers.

Advanced Validation (FluentValidation):

Cross-Property Validation: Logic to ensure SalesPrice is always greater than BuyingPrice.

Database-Level Validation: Custom validators injected with Repositories to check for Unique SKU (Stock Keeping Unit) existence in real-time before database insertion.

Relational Data: One-to-Many relationship configuration between Products and Categories.

## Tech Stack
Framework: ASP.NET Core 8.0 MVC

Language: C#

Database: MS SQL Server

ORM: Entity Framework Core (Code First Approach)

Validation: FluentValidation.AspNetCore

Frontend: Razor Views, Bootstrap 5

IDE: Visual Studio 2022