# Unified Search API Implementation Plan

## 🎯 **Overview**

This document outlines the implementation plan for a macOS Spotlight-like unified search API for the OpenA3XX system. The search will span across all entity types and provide fast, relevant results with advanced filtering capabilities.

## 📊 **Searchable Entities**

### **Primary Entities (High Priority)**
1. **Aircraft Models** - Name, Type, Description, Manufacturer
2. **Hardware Panels** - Name, Description, Owner
3. **Hardware Boards** - Name, Description
4. **Hardware Inputs/Outputs** - Name, Description, Type
5. **Simulator Events** - Event Code, Friendly Name, Description
6. **Manufacturers** - Name

### **Secondary Entities (Medium Priority)**
7. **Hardware Input/Output Types** - Name, Description
8. **Hardware Input/Output Selectors** - Name, Description
9. **Hardware Panel Tokens** - Device Token, IP Address
10. **HubHop Presets** - Name, Path, Vendor, Aircraft

## 🏗️ **Architecture**

### **Service Layer**
```
src/OpenA3XX.Core/Services/Search/
├── ISearchService.cs ✅
├── SearchService.cs (to implement)
├── ISearchIndexService.cs (future)
├── SearchIndexService.cs (future)
└── Models/
    ├── SearchResultDto.cs ✅
    ├── SearchQueryDto.cs ✅
    └── SearchResponseDto.cs ✅
```

### **Repository Layer**
```
src/OpenA3XX.Core/Repositories/Search/
├── ISearchRepository.cs ✅
└── SearchRepository.cs (to implement)
```

### **Controller Layer**
```
src/OpenA3XX.Peripheral.WebApi/Controllers/
└── SearchController.cs ✅
```

## 🔍 **Search Features**

### **1. Basic Search**
- ✅ **Full-text search** across all entity names and descriptions
- ✅ **Fuzzy matching** for typos and partial matches
- ✅ **Case-insensitive** search
- ✅ **Multi-term** search (e.g., "boeing 737 panel")

### **2. Advanced Search**
- ✅ **Entity type filtering** (e.g., only aircraft models)
- ✅ **Date range filtering** (created/updated dates)
- ✅ **Status filtering** (active/inactive entities)
- ✅ **Manufacturer filtering** (for aircraft models)

### **3. Search Results**
- ✅ **Relevance scoring** based on match quality
- ✅ **Snippets** showing matched text with context
- ✅ **Entity type indicators** (icons/categories)
- ✅ **Quick actions** (view, edit, delete)

## 📋 **Implementation Phases**

### **Phase 1: Core Infrastructure** ✅
- [x] Create Search DTOs
- [x] Create Search Service Interface
- [x] Create Search Repository Interface
- [x] Create Search Controller

### **Phase 2: Repository Implementation** 🔄
- [ ] Implement SearchRepository
- [ ] Entity-specific search methods
- [ ] SQL queries for performance
- [ ] Relevance scoring algorithm

### **Phase 3: Service Implementation** 🔄
- [ ] Implement SearchService
- [ ] Unified search logic
- [ ] Result aggregation and sorting
- [ ] Facet generation

### **Phase 4: Advanced Features** 📋
- [ ] Search indexing service
- [ ] Real-time index updates
- [ ] Search result caching
- [ ] Search analytics

## 🎨 **API Design**

### **Search Endpoints**

#### **1. Main Search**
```http
GET /api/search?q={query}&type={entityType}&limit={limit}&offset={offset}
```

**Parameters:**
- `q` - Search query text (required)
- `type` - Entity type filter (comma-separated)
- `limit` - Maximum results (1-100, default: 20)
- `offset` - Results offset (default: 0)
- `minScore` - Minimum relevance score (0.0-1.0, default: 0.1)
- `includeInactive` - Include inactive entities (default: false)
- `fromDate` - Filter by creation date from
- `toDate` - Filter by creation date to
- `manufacturer` - Filter by manufacturer
- `sortBy` - Sort order (relevance, title, createdDate, updatedDate, entityType)
- `includeFacets` - Include facets in response (default: true)

#### **2. Quick Search**
```http
GET /api/search/quick?q={query}&limit={limit}
```

#### **3. Search Suggestions**
```http
GET /api/search/suggestions?q={query}&limit={limit}
```

#### **4. Entity Types**
```http
GET /api/search/entity-types
```

#### **5. Search Statistics**
```http
GET /api/search/statistics
```

### **Response Format**
```json
{
  "query": "boeing 737",
  "totalResults": 15,
  "page": 1,
  "totalPages": 2,
  "executionTimeMs": 45,
  "isSuccess": true,
  "results": [
    {
      "id": "aircraft-123",
      "entityType": "AircraftModel",
      "title": "Boeing 737-800",
      "description": "Commercial airliner",
      "manufacturer": "Boeing",
      "relevanceScore": 0.95,
      "snippet": "Boeing 737-800 commercial airliner...",
      "metadata": {
        "isActive": true,
        "createdAt": "2024-01-15T10:30:00Z"
      },
      "actions": [
        {
          "name": "View",
          "url": "/api/aircraft-models/123",
          "method": "GET",
          "requiresConfirmation": false
        },
        {
          "name": "Edit",
          "url": "/api/aircraft-models/123",
          "method": "PUT",
          "requiresConfirmation": false
        }
      ],
      "createdAt": "2024-01-15T10:30:00Z",
      "updatedAt": "2024-01-20T14:45:00Z"
    }
  ],
  "facets": {
    "entityTypes": {
      "AircraftModel": 5,
      "HardwarePanel": 3,
      "SimulatorEvent": 7
    },
    "manufacturers": {
      "Boeing": 3,
      "Airbus": 2
    },
    "hardwareTypes": {
      "Input": 8,
      "Output": 4
    },
    "simulatorSdkTypes": {
      "SimConnect": 5,
      "FSUIPC": 2
    },
    "dateRanges": {
      "Last 7 days": 2,
      "Last 30 days": 8,
      "Last 90 days": 5
    }
  }
}
```

## 🚀 **Next Steps**

### **Immediate Tasks**
1. **Implement SearchRepository**
   - Create SQL queries for each entity type
   - Implement relevance scoring
   - Add facet generation

2. **Implement SearchService**
   - Unified search logic
   - Result aggregation
   - Sorting and filtering

3. **Add Service Registration**
   - Register in ServiceCollectionExtensions
   - Add dependency injection

4. **Testing**
   - Unit tests for search logic
   - Integration tests for API endpoints
   - Performance testing

### **Future Enhancements**
1. **Search Indexing**
   - Background indexing service
   - Real-time index updates
   - Search result caching

2. **Advanced Features**
   - Search analytics
   - Popular search terms
   - Search performance metrics

3. **UI Integration**
   - Frontend search component
   - Autocomplete functionality
   - Search result display

## 📈 **Performance Considerations**

### **Database Optimization**
- Use full-text search indexes
- Implement query optimization
- Add result caching

### **Search Algorithm**
- Relevance scoring based on:
  - Exact matches (highest score)
  - Partial matches
  - Fuzzy matches
  - Field importance (title > description)

### **Caching Strategy**
- Cache frequent search results
- Cache entity type counts
- Cache search suggestions

## 🔧 **Technical Implementation**

### **Search Repository Implementation**
```csharp
public class SearchRepository : ISearchRepository
{
    // Entity-specific search methods
    // SQL queries with full-text search
    // Relevance scoring algorithm
    // Facet generation
}
```

### **Search Service Implementation**
```csharp
public class SearchService : ISearchService
{
    // Unified search logic
    // Result aggregation
    // Sorting and filtering
    // Facet generation
}
```

## 📝 **Testing Strategy**

### **Unit Tests**
- Search repository methods
- Search service logic
- Relevance scoring algorithm

### **Integration Tests**
- API endpoint testing
- Database query testing
- Performance testing

### **Manual Testing**
- Search functionality testing
- UI integration testing
- Performance validation

## 🎯 **Success Metrics**

### **Performance Targets**
- Search response time: < 100ms
- Relevance accuracy: > 90%
- Search coverage: All entity types

### **User Experience**
- Intuitive search interface
- Fast autocomplete
- Relevant search results
- Easy filtering options

This unified search API will provide a powerful, macOS Spotlight-like search experience across all OpenA3XX entities, making it easy for users to find and access any system component quickly and efficiently. 