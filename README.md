# Multi-DB Discovery API

## Project Overview
This project is a specialized inspection tool built with ASP.NET Core. it connects to two separate SQL Server databases (Azure SQL Elastic Pool and a standard SQL Server) to provide real-time insights into database schema, including table names and current row counts.

**Strictly Read-Only**: This API is configured for discovery and inspection. All data-modifying operations (POST, PUT, DELETE) have been removed to ensure the safety of sensitive database contents.

## Tech Stack
- **Framework**: .NET 9.0 (ASP.NET Core Web API)
- **ORM**: Entity Framework Core
- **Database**: SQL Server
- **UI**: Scalar API Reference (Modern OpenAPI UI)

## Database Setup
The application uses two connection strings defined in `appsettings.json`.

```json
"ConnectionStrings": {
  "ElasticPoolDb": "...",
  "SqlServerDb": "..."
}
```

## How to Run the API locally
1. Ensure you have the .NET 9 SDK installed.
2. Run the command:
   ```bash
   dotnet run
   ```
3. The API will automatically open the **Scalar UI** in your default browser at:
   `http://localhost:5089/scalar/v1`

## API Endpoints

### Database Inspection (`api/db/tables`)
This is the primary endpoint for discovering existing tables and verifying row counts across both databases.

**Sample Response:**
```json
{
  "elasticPoolInfo": [{ "tableName": "ShardMapManagerGlobal", "rowCounts": 1, ... }],
  "sqlServerInfo": [{ "tableName": "UserMgmt_Users", "rowCounts": 42, ... }]
}
```

## Folder Structure
- **/Controllers**: Contains `DatabaseInfoController.cs` for discovery logic.
- **Program.cs**: Application configuration, DI for multiple DbContexts, and Scalar UI setup.
- **appsettings.json**: Secure storage for your connection strings.
- **api.md**: Dedicated technical guide for Next.js frontend integration.

## Next.js Integration
The API includes a CORS policy that allows requests from `http://localhost:3000`. Refer to [api.md](./api.md) for fetch examples and data models.
