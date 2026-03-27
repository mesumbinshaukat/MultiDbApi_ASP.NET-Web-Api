# API Documentation for Database Discovery

This document provides a technical reference for the read-only inspection and discovery API.

## Base URL
Default: `http://localhost:5089` (or `https://localhost:7253`)

---

## 1. Database Inspection (`api/db/tables`)
Route: `/api/db/tables`

Fetches the physical table structure, schema names, and current row counts from both connected databases.

**Response (200 OK):**
```json
{
  "elasticPoolInfo": [
    {
      "tableName": "ShardMapManagerGlobal",
      "schemaName": "dbo",
      "rowCounts": 1
    }
  ],
  "sqlServerInfo": [
    {
      "tableName": "UserMgmt_Users",
      "schemaName": "dbo",
      "rowCounts": 42
    }
  ]
}
```

---

## Discovered Tables Reference

As of the last inspection, the following tables were discovered:

### ElasticPoolDb
- ShardMapManagerGlobal
- ShardMapsGlobal
- ShardsGlobal
- ShardMappingsGlobal
- OperationsLogGlobal
- ShardedDatabaseSchemaInfosGlobal

### SqlServerDb
- Template_Master
- Profile_Business
- Profile_BusinessContacts
- UserMgmt_Invitation
- UserMgmt_Users
- RolePermissions
- Device_Stations
- UserMgmt_Roles
- UserMgmt_Permissions
- UserMgmt_UserRoles
- UserMgmt_UserPermissionOverrides
- Profile_BusinessTypes

---

## Next.js Consumption Guide (Read-Only)

### GET Example: Fetch Table Stats
```javascript
export async function getDatabaseStats() {
  const res = await fetch('http://localhost:5089/api/db/tables', {
    cache: 'no-store'
  });
  if (!res.ok) throw new Error('Failed to fetch database stats');
  return res.json();
}
```

## Error Codes
- **500 Internal Server Error**: Database connectivity issue or permission denied.
