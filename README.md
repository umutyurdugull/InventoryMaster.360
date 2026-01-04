## InventoryMaster 360
A robust, N-Tier Architecture based Product Management System built with ASP.NET Core MVC. This project simulates a real-world back-office application, focusing on strict business logic, data integrity, and clean code principles.

## Overview
InventoryMaster 360 is designed to manage product lifecycles with high precision. Unlike standard CRUD applications, this system enforces strict validation rules to prevent data corruption and logical errors (e.g., selling below cost). It utilizes advanced Entity Framework Core features like Global Query Filters for soft deletes and Interceptors for automatic auditing.

## Key Features
# Dashboard & Analytics
Real-time Metrics: Displays total active products, critical stock alerts, and total inventory value.

Visual Alerts: Highlights products with stock levels below the critical threshold.

# Advanced Product Management
Smart SKU System: Supports custom SKU entry with uniqueness checks.

Soft Delete: Implemented using EF Core Global Query Filters. Deleted items are marked as passive instead of being removed from the database.

Image Handling: File upload capability with unique GUID naming and default placeholders for missing images.

Pricing Logic: Prevents negative pricing and ensures SalesPrice is never lower than BuyingPrice.

# Robust Validation (Business Logic)
Powered by FluentValidation to keep controllers clean:

Cross-Property Validation: Ensures logical consistency between related fields (e.g., Cost vs. Price).

Database-Level Validation: Checks for duplicate SKUs before hitting the database.

Data Integrity: Prevents deletion of Categories that contain active Products (Orphan check).

# Search & Filtering
Server-Side Pagination: Efficiently handles large datasets using Skip and Take.

Dynamic Filtering: Filter products by Category.

Live Search: Find products instantly by name or SKU.

# Architecture & Database
N-Tier Architecture: Separation of concerns (Core, Data, Service, Web).

Auditing: Automatic tracking of CreatedDate and UpdatedDate via overridden SaveChanges.

One-to-Many Relationships: properly configured relationships between Products and Categories.


## Tech Stack
Framework: ASP.NET Core 8.0 MVC

Language: C#

Database: MS SQL Server

ORM: Entity Framework Core (Code First)

Validation: FluentValidation.AspNetCore

Frontend: Razor Views, Bootstrap 5, jQuery

Tools: Visual Studio 2022 / VS Code