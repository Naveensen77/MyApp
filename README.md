# MyApp — Modular Monolith Architecture

Welcome to **MyApp**, a high-performance, modular enterprise web application built on **.NET 10**. This project leverages clean architecture principles, CQRS database segregation, and automatic source generators to produce a maintainable, fast, and scalable codebase.

---

## 1. Project Directory Structure & Layers

Each domain area is encapsulated inside a self-contained **Module** consisting of 4 distinct project layers:

```
MyApp.DomainName.Core           ← Domain Entities, Value Objects, DTOs, EF Configurations, Abstract DbContext
MyApp.DomainName.Application    ← CQRS Features (Commands, Queries, Handlers, Validators)
MyApp.DomainName.Infrastructure ← Concrete DbContexts, Database Seeding, SQL Connection Wiring
MyApp.DomainName.Endpoints      ← Minimal APIs, Route Grouping, Endpoint Registrations, Module DI Extension
```

### Dependency Rules (Strict Flow)
```
Endpoints ──> Application ──> Core
     │                          ▲
     └───> Infrastructure ──────┘
```
*   **Endpoints** references all other layers to act as the module entry point and map HTTP requests.
*   **Application** only references **Core** (keeping it free of infrastructure details like EF Core database configurations).
*   **Infrastructure** only references **Core** to wire up database settings.
*   **Core** has no internal references, only sharing a reference to `MyApp.Common.Core`.

---

## 2. CQRS & DbContext Segregation

To optimize query speeds and maintain strict control over transactions, each module implements two separate DbContexts inheriting from a single base DbContext:

1.  **Command DbContext (Write Context)**
    *   Used for INSERT, UPDATE, and DELETE operations.
    *   Tracks entity changes.
    *   Has `AuditCommandInterceptor` attached to automatically stamp creator details, timestamp, and IP address.
2.  **Query DbContext (Read Context)**
    *   Used for read-only SELECT operations.
    *   Tracking is completely disabled (`QueryTrackingBehavior.NoTracking`) for maximum query execution performance.

---

## 3. Entity Auditing

Entities inheriting from `AuditEntity` automatically capture audit trail parameters without manual assignment:
*   `CreatedBy`: Username of the user making changes.
*   `CreatedOn`: UTC Timestamp when changes were made.
*   `IpAddress`: The remote IP address of the client request.

This is executed transparently via the `AuditCommandInterceptor` hooked into the EF Core pipeline.

---

## 4. Entity Framework Core Migrations Guide

Because the application is structured as a Modular Monolith with **multiple independent DbContexts**, you must run CLI migrations explicitly by specifying the context, Target project, and Startup project.

### Prerequisites
Ensure the global Entity Framework CLI tools are installed:
```powershell
dotnet tool install --global dotnet-ef
```

### Migration Commands (Run from Solution Root: `D:\Naveen\MyApp\MyApp`)

#### 1. Add a Migration (e.g. for CareerAdvancement module)
```powershell
dotnet ef migrations add <MigrationName> `
  --context CareerCommandDbContext `
  --project MyApp.CareerAdvancement.Core `
  --startup-project MyApp.Api
```

#### 2. Apply Migration to Database
```powershell
dotnet ef database update `
  --context CareerCommandDbContext `
  --project MyApp.CareerAdvancement.Core `
  --startup-project MyApp.Api
```

#### 3. Remove the Last Migration (Before applying to Database)
```powershell
dotnet ef migrations remove `
  --context CareerCommandDbContext `
  --project MyApp.CareerAdvancement.Core `
  --startup-project MyApp.Api
```

#### 4. Generate SQL Script
If you need to generate raw SQL scripts to hand over to a DBA or deploy via CI/CD pipelines:
```powershell
dotnet ef migrations script `
  --context CareerCommandDbContext `
  --project MyApp.CareerAdvancement.Core `
  --startup-project MyApp.Api
```

---

## 5. Adding a New Module Checklist

When extending the system with a new domain module, follow this recipe:

1.  **Scaffold Projects**: Create the four projects (`Core`, `Application`, `Infrastructure`, `Endpoints`).
2.  **Set Up References**: Follow the dependency layout graph shown above.
3.  **Define Schema**: Add your module schema name to `SchemaNames.cs` (e.g., `public const string MyNewModule = nameof(MyNewModule);`).
4.  **Create DbContexts**: Inherit from `BaseDbContext` in Core, then implement Concrete command/query contexts.
5.  **Write Entities**: Inherit from `AuditEntity` and configure in `Data/Configurations/`.
6.  **Wire Infrastructure**: Implement `ServiceCollectionExtensions` inside the new Infrastructure project to register command/query contexts.
7.  **Write Endpoints**: Implement `IApiEndpoint` and add route mappings using the group mappers.
8.  **Wired in API host**: Reference the new Endpoints project in `MyApp.Api.csproj` and call your extensions in `Program.cs`.
