# CarRental

CarRental is a sample .NET 8 project that demonstrates a car rental domain built with Domain-Driven Design patterns. The application models cars, drivers, rentals, pricing, free-minute bonuses, domain events, repositories, and a unit-of-work based persistence layer.

The repository includes a console scenario that creates a car and a driver, rents the car, returns it, calculates the rental cost, updates the car position, and prints the final state.

## Project Structure

```text
CarRental.sln
DDD.SharedKernel/              Shared DDD building blocks
DDD.CarRental.Core/            Main car rental domain, application, and infrastructure code
DDD.CarRental.ConsoleTest/     Console runner with an end-to-end scenario
```

## Main Components

- `DDD.SharedKernel` contains reusable abstractions and base types, including entities, value objects, domain events, repositories, unit of work, and event publishing contracts.
- `DDD.CarRental.Core` contains the car rental bounded context:
  - `DomainModelLayer` includes aggregates, value objects, factories, policies, domain services, repository interfaces, and domain events.
  - `ApplicationLayer` includes commands, queries, DTOs, mappers, command handlers, query handlers, and domain event handlers.
  - `InfrastructureLayer` includes Entity Framework Core persistence, SQLite configuration, repositories, unit of work, and a simple position service.
- `DDD.CarRental.ConsoleTest` wires the application with dependency injection and runs a complete rental flow.

## Technologies

- .NET 8
- C#
- Entity Framework Core
- SQLite
- Dapper
- Microsoft.Extensions.DependencyInjection

## Requirements

Install the .NET 8 SDK:

```bash
dotnet --version
```

The command should print an installed .NET SDK version compatible with `net8.0`.

## Getting Started

Restore dependencies:

```bash
dotnet restore
```

Build the solution:

```bash
dotnet build
```

Run the console scenario:

```bash
dotnet run --project DDD.CarRental.ConsoleTest
```

## Console Scenario

The console runner performs this flow:

1. Creates a car with a registration number, daily rate, and initial position.
2. Creates a driver with a license number and free-minute balance.
3. Rents the car to the driver.
4. Returns the car.
5. Updates the car position.
6. Calculates the rental total.
7. Prints the final car, driver, and rental information.

Example output includes messages such as:

```text
=== CarRental E2E scenario ===
Utworzono samochod: ...
Utworzono kierowce: ...
Wynajeto samochod. ...
Zwrocono samochod. ...
Koszt wynajmu: ...
=== End ===
```

## Database

The console application uses SQLite with the connection string:

```text
Data Source=car-rental-test.db
```

`CarRentalDbContext` calls `EnsureDeleted()` and `EnsureCreated()` when it is constructed, so the database is recreated for each run. This keeps the console scenario deterministic, but it also means data is not preserved between runs.

## Domain Highlights

- Cars have a rental status and position.
- Drivers can receive free minutes after completed rentals.
- Rentals are created through a domain factory and finished through `RentalService`.
- Pricing is calculated by `RentalPricingPolicy`.
- Free-minute rewards are calculated by `FreeMinutesPolicy`.
- Domain events are published through the shared kernel event publisher abstraction.
- Persistence is hidden behind repositories and `ICarRentalUnitOfWork`.

## Useful Commands

```bash
dotnet restore
dotnet build
dotnet run --project DDD.CarRental.ConsoleTest
```

## Notes

- The project currently exposes behavior through the console runner rather than a web API or graphical interface.
- Generated build outputs in `bin/` and `obj/` are not required to understand or run the source code.
- The repository includes a root-level `car-rental-test.db`, but the console runner can recreate its SQLite database automatically.
