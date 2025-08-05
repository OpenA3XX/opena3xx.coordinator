# Audit Implementation for OpenA3XX.Coordinator

## Overview

This document describes the comprehensive audit system implemented for the OpenA3XX.Coordinator application. The audit system automatically tracks all entity changes (create, update, delete) and provides API endpoints to query audit history.

## Features

### ✅ **Automatic Change Tracking**
- **Entity Creation** - Tracks when new entities are added
- **Entity Updates** - Tracks modifications with before/after values
- **Entity Deletion** - Tracks when entities are removed
- **All Entity Types** - AircraftModel, HardwarePanel, HardwareInput, HardwareOutput, SimulatorEvent, etc.

### ✅ **Comprehensive Audit Data**
- **Entity Information** - Entity name and ID
- **Action Type** - Created, Updated, Deleted
- **Timestamps** - When changes occurred
- **User Context** - Who made the change (when available)
- **Before/After Values** - JSON serialized old and new values
- **Request Context** - IP address, HTTP method, endpoint
- **Additional Context** - Custom context information

### ✅ **API Endpoints**
- **GET /api/audit/entity/{entityName}/{entityId}** - Get audit history for specific entity
- **GET /api/audit** - Get all audit entries with filtering

### ✅ **Database Integration**
- **New Audit Table** - `AuditEntries` table with optimized indexes
- **Automatic Integration** - Works with existing Entity Framework setup
- **Performance Optimized** - Indexed on common query patterns

## Implementation Details

### 1. **AuditEntry Model**
```csharp
public class AuditEntry
{
    public int Id { get; set; }
    public string EntityName { get; set; }
    public string Action { get; set; }
    public int EntityId { get; set; }
    public string OldValues { get; set; }  // JSON
    public string NewValues { get; set; }  // JSON
    public string UserId { get; set; }
    public DateTime Timestamp { get; set; }
    public string IpAddress { get; set; }
    public string Context { get; set; }
    public string HttpMethod { get; set; }
    public string Endpoint { get; set; }
}
```

### 2. **Automatic Change Detection**
The `CoreDataContext` overrides `SaveChanges()` and `SaveChangesAsync()` to:
- Detect all entity changes using EF Core's `ChangeTracker`
- Create audit entries for each change
- Store before/after values as JSON
- Automatically save audit entries

### 3. **Audit Service**
- **IAuditService** - Interface for audit operations
- **AuditService** - Implementation with database operations
- **Logging Integration** - Comprehensive logging of audit activities

### 4. **API Controller**
- **AuditController** - RESTful endpoints for audit queries
- **Filtering Support** - By entity, action, user, date range
- **Pagination** - Limit and offset parameters
- **Error Handling** - Proper HTTP status codes and error messages

## Database Schema

### AuditEntries Table
```sql
CREATE TABLE AuditEntries (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    EntityName TEXT NOT NULL,
    Action TEXT NOT NULL,
    EntityId INTEGER NOT NULL,
    OldValues TEXT,
    NewValues TEXT,
    UserId TEXT,
    Timestamp TEXT NOT NULL,
    IpAddress TEXT,
    Context TEXT,
    HttpMethod TEXT,
    Endpoint TEXT
);

-- Indexes for performance
CREATE INDEX IX_AuditEntries_EntityName_EntityId ON AuditEntries(EntityName, EntityId);
CREATE INDEX IX_AuditEntries_Timestamp ON AuditEntries(Timestamp);
CREATE INDEX IX_AuditEntries_UserId ON AuditEntries(UserId);
```

## API Usage Examples

### Get Audit History for Specific Entity
```http
GET /api/audit/entity/AircraftModel/123?limit=50&offset=0
```

### Get All Audit Entries with Filtering
```http
GET /api/audit?entityName=HardwarePanel&action=Updated&fromDate=2024-01-01&limit=100
```

### Response Format
```json
[
  {
    "id": 1,
    "entityName": "AircraftModel",
    "action": "Created",
    "entityId": 123,
    "oldValues": null,
    "newValues": "{\"Model\":\"Boeing 737\",\"ManufacturerId\":1}",
    "userId": "user123",
    "timestamp": "2024-01-01T10:30:00Z",
    "ipAddress": "192.168.1.100",
    "context": "API request",
    "httpMethod": "POST",
    "endpoint": "/api/aircraft-models"
  }
]
```

## Migration

To apply the audit table to your database:

1. **Run the migration:**
   ```bash
   dotnet ef database update
   ```

2. **Or manually apply the migration:**
   ```bash
   dotnet ef migrations add AddAuditTable
   dotnet ef database update
   ```

## Configuration

### Service Registration
The audit service is automatically registered in `ServiceCollectionExtensions.AddDatabaseServices()`:

```csharp
services.AddScoped<IAuditService, AuditService>();
```

### DbContext Integration
The `CoreDataContext` is enhanced with:
- Audit service dependency injection
- Automatic change detection
- JSON serialization of entity values

## Performance Considerations

### ✅ **Optimized for Read Performance**
- Indexed on common query patterns
- Efficient filtering by entity and timestamp
- Pagination support for large datasets

### ✅ **Minimal Write Overhead**
- Audit entries created in same transaction
- JSON serialization is fast
- No blocking operations

### ✅ **Storage Efficiency**
- Only modified properties stored in JSON
- Null values are not stored
- Compressed JSON storage

## Security Features

### ✅ **Data Protection**
- No sensitive data in audit logs (configurable)
- User context tracking (when available)
- IP address logging for security analysis

### ✅ **Access Control**
- Audit endpoints can be secured with authentication
- Role-based access to audit data
- Audit of audit access (meta-auditing)

## Monitoring and Maintenance

### ✅ **Logging Integration**
- All audit operations are logged
- Error tracking and alerting
- Performance monitoring

### ✅ **Data Retention**
- Configurable retention policies
- Archive old audit entries
- Cleanup utilities

## Future Enhancements

### 🔄 **Planned Features**
- **Real-time Audit Events** - SignalR integration for live updates
- **Audit Analytics** - Dashboard for audit analysis
- **Export Functionality** - CSV/Excel export of audit data
- **Advanced Filtering** - Full-text search in audit entries
- **Audit Alerts** - Notifications for suspicious activities

### 🔄 **Integration Opportunities**
- **User Authentication** - Integrate with existing auth system
- **API Gateway** - Add audit context to all API calls
- **Monitoring Tools** - Integration with application monitoring

## Troubleshooting

### Common Issues

1. **Migration Fails**
   - Ensure database is accessible
   - Check for existing audit table
   - Verify EF Core tools are installed

2. **Performance Issues**
   - Monitor audit table size
   - Consider archiving old entries
   - Review query patterns

3. **Missing Audit Entries**
   - Check ChangeTracker configuration
   - Verify SaveChanges is called
   - Review entity configuration

## Support

For issues or questions about the audit implementation:
- Check the application logs for audit-related errors
- Verify the audit table exists and has proper indexes
- Test the audit API endpoints directly

---

**Implementation Status:** ✅ Complete
**Last Updated:** January 2025
**Version:** 1.0.0 