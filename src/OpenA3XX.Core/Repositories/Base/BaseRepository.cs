using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace OpenA3XX.Core.Repositories.Base
{
    /// <summary>
    /// Base repository implementation providing common data access operations.
    /// Does not automatically save changes - use Unit of Work pattern or call SaveChanges explicitly.
    /// </summary>
    /// <typeparam name="T">The entity type</typeparam>
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        /// <summary>
        /// The database context for this repository
        /// </summary>
        protected readonly DbContext Context;
        
        /// <summary>
        /// Logger for database operations
        /// </summary>
        protected readonly ILogger<BaseRepository<T>> Logger;

        /// <summary>
        /// Initializes a new instance of the BaseRepository
        /// </summary>
        /// <param name="context">The database context</param>
        /// <param name="logger">The logger instance</param>
        protected BaseRepository(DbContext context, ILogger<BaseRepository<T>> logger)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Gets a queryable collection of all entities
        /// </summary>
        /// <returns>IQueryable of entities</returns>
        public virtual IQueryable<T> GetAll()
        {
            var stopwatch = Stopwatch.StartNew();
            var entityType = typeof(T).Name;
            
            Logger.LogInformation("Database Read Operation Started - Entity: {EntityType}, Operation: {Operation}",
                entityType, "GetAll");

            try
            {
                var result = Context.Set<T>();
                stopwatch.Stop();
                
                Logger.LogInformation("Database Read Operation Completed - Entity: {EntityType}, Operation: {Operation}, Duration: {Duration}ms",
                    entityType, "GetAll", stopwatch.ElapsedMilliseconds);
                
                return result;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                Logger.LogError(ex, "Database Read Operation Failed - Entity: {EntityType}, Operation: {Operation}, Duration: {Duration}ms",
                    entityType, "GetAll", stopwatch.ElapsedMilliseconds);
                throw;
            }
        }

        /// <summary>
        /// Gets all entities as a list asynchronously
        /// </summary>
        /// <returns>Collection of all entities</returns>
        public virtual async Task<ICollection<T>> GetAllAsync()
        {
            var stopwatch = Stopwatch.StartNew();
            var entityType = typeof(T).Name;
            
            Logger.LogInformation("Database Read Operation Started - Entity: {EntityType}, Operation: {Operation}",
                entityType, "GetAllAsync");

            try
            {
                var result = await Context.Set<T>().ToListAsync();
                stopwatch.Stop();
                
                Logger.LogInformation("Database Read Operation Completed - Entity: {EntityType}, Operation: {Operation}, Duration: {Duration}ms, RecordCount: {RecordCount}",
                    entityType, "GetAllAsync", stopwatch.ElapsedMilliseconds, result.Count);
                
                return result;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                Logger.LogError(ex, "Database Read Operation Failed - Entity: {EntityType}, Operation: {Operation}, Duration: {Duration}ms",
                    entityType, "GetAllAsync", stopwatch.ElapsedMilliseconds);
                throw;
            }
        }

        /// <summary>
        /// Gets an entity by its ID
        /// </summary>
        /// <param name="id">The entity ID</param>
        /// <returns>The entity or null if not found</returns>
        public virtual T Get(int id)
        {
            var stopwatch = Stopwatch.StartNew();
            var entityType = typeof(T).Name;
            
            Logger.LogInformation("Database Read Operation Started - Entity: {EntityType}, Operation: {Operation}, EntityId: {EntityId}",
                entityType, "Get", id);

            try
            {
                var result = Context.Set<T>().Find(id);
                stopwatch.Stop();
                
                Logger.LogInformation("Database Read Operation Completed - Entity: {EntityType}, Operation: {Operation}, EntityId: {EntityId}, Duration: {Duration}ms, Found: {Found}",
                    entityType, "Get", id, stopwatch.ElapsedMilliseconds, result != null);
                
                return result;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                Logger.LogError(ex, "Database Read Operation Failed - Entity: {EntityType}, Operation: {Operation}, EntityId: {EntityId}, Duration: {Duration}ms",
                    entityType, "Get", id, stopwatch.ElapsedMilliseconds);
                throw;
            }
        }

        /// <summary>
        /// Gets an entity by its ID asynchronously
        /// </summary>
        /// <param name="id">The entity ID</param>
        /// <returns>The entity or null if not found</returns>
        public virtual async Task<T> GetAsync(int id)
        {
            var stopwatch = Stopwatch.StartNew();
            var entityType = typeof(T).Name;
            
            Logger.LogInformation("Database Read Operation Started - Entity: {EntityType}, Operation: {Operation}, EntityId: {EntityId}",
                entityType, "GetAsync", id);

            try
            {
                var result = await Context.Set<T>().FindAsync(id);
                stopwatch.Stop();
                
                Logger.LogInformation("Database Read Operation Completed - Entity: {EntityType}, Operation: {Operation}, EntityId: {EntityId}, Duration: {Duration}ms, Found: {Found}",
                    entityType, "GetAsync", id, stopwatch.ElapsedMilliseconds, result != null);
                
                return result;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                Logger.LogError(ex, "Database Read Operation Failed - Entity: {EntityType}, Operation: {Operation}, EntityId: {EntityId}, Duration: {Duration}ms",
                    entityType, "GetAsync", id, stopwatch.ElapsedMilliseconds);
                throw;
            }
        }

        /// <summary>
        /// Adds an entity to the context without saving
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <returns>The added entity</returns>
        public virtual T Add(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
                
            Context.Set<T>().Add(entity);
            return entity;
        }

        /// <summary>
        /// Adds an entity to the context asynchronously without saving
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <returns>The added entity</returns>
        public virtual Task<T> AddAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
                
            Context.Set<T>().Add(entity);
            return Task.FromResult(entity);
        }

        /// <summary>
        /// Finds a single entity matching the predicate
        /// </summary>
        /// <param name="match">The predicate to match</param>
        /// <returns>The entity or null if not found</returns>
        public virtual T Find(Expression<Func<T, bool>> match)
        {
            var stopwatch = Stopwatch.StartNew();
            var entityType = typeof(T).Name;
            
            Logger.LogInformation("Database Read Operation Started - Entity: {EntityType}, Operation: {Operation}, Predicate: {Predicate}",
                entityType, "Find", match.ToString());

            try
            {
                var result = Context.Set<T>().SingleOrDefault(match);
                stopwatch.Stop();
                
                Logger.LogInformation("Database Read Operation Completed - Entity: {EntityType}, Operation: {Operation}, Duration: {Duration}ms, Found: {Found}",
                    entityType, "Find", stopwatch.ElapsedMilliseconds, result != null);
                
                return result;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                Logger.LogError(ex, "Database Read Operation Failed - Entity: {EntityType}, Operation: {Operation}, Duration: {Duration}ms, Predicate: {Predicate}",
                    entityType, "Find", stopwatch.ElapsedMilliseconds, match.ToString());
                throw;
            }
        }

        /// <summary>
        /// Finds a single entity matching the predicate asynchronously
        /// </summary>
        /// <param name="match">The predicate to match</param>
        /// <returns>The entity or null if not found</returns>
        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> match)
        {
            var stopwatch = Stopwatch.StartNew();
            var entityType = typeof(T).Name;
            
            Logger.LogInformation("Database Read Operation Started - Entity: {EntityType}, Operation: {Operation}, Predicate: {Predicate}",
                entityType, "FindAsync", match.ToString());

            try
            {
                var result = await Context.Set<T>().SingleOrDefaultAsync(match);
                stopwatch.Stop();
                
                Logger.LogInformation("Database Read Operation Completed - Entity: {EntityType}, Operation: {Operation}, Duration: {Duration}ms, Found: {Found}",
                    entityType, "FindAsync", stopwatch.ElapsedMilliseconds, result != null);
                
                return result;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                Logger.LogError(ex, "Database Read Operation Failed - Entity: {EntityType}, Operation: {Operation}, Duration: {Duration}ms, Predicate: {Predicate}",
                    entityType, "FindAsync", stopwatch.ElapsedMilliseconds, match.ToString());
                throw;
            }
        }

        /// <summary>
        /// Finds all entities matching the predicate
        /// </summary>
        /// <param name="match">The predicate to match</param>
        /// <returns>Collection of matching entities</returns>
        public virtual ICollection<T> FindAll(Expression<Func<T, bool>> match)
        {
            var stopwatch = Stopwatch.StartNew();
            var entityType = typeof(T).Name;
            
            Logger.LogInformation("Database Read Operation Started - Entity: {EntityType}, Operation: {Operation}, Predicate: {Predicate}",
                entityType, "FindAll", match.ToString());

            try
            {
                var result = Context.Set<T>().Where(match).ToList();
                stopwatch.Stop();
                
                Logger.LogInformation("Database Read Operation Completed - Entity: {EntityType}, Operation: {Operation}, Duration: {Duration}ms, RecordCount: {RecordCount}",
                    entityType, "FindAll", stopwatch.ElapsedMilliseconds, result.Count);
                
                return result;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                Logger.LogError(ex, "Database Read Operation Failed - Entity: {EntityType}, Operation: {Operation}, Duration: {Duration}ms, Predicate: {Predicate}",
                    entityType, "FindAll", stopwatch.ElapsedMilliseconds, match.ToString());
                throw;
            }
        }

        /// <summary>
        /// Finds all entities matching the predicate asynchronously
        /// </summary>
        /// <param name="match">The predicate to match</param>
        /// <returns>Collection of matching entities</returns>
        public virtual async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match)
        {
            var stopwatch = Stopwatch.StartNew();
            var entityType = typeof(T).Name;
            
            Logger.LogInformation("Database Read Operation Started - Entity: {EntityType}, Operation: {Operation}, Predicate: {Predicate}",
                entityType, "FindAllAsync", match.ToString());

            try
            {
                var result = await Context.Set<T>().Where(match).ToListAsync();
                stopwatch.Stop();
                
                Logger.LogInformation("Database Read Operation Completed - Entity: {EntityType}, Operation: {Operation}, Duration: {Duration}ms, RecordCount: {RecordCount}",
                    entityType, "FindAllAsync", stopwatch.ElapsedMilliseconds, result.Count);
                
                return result;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                Logger.LogError(ex, "Database Read Operation Failed - Entity: {EntityType}, Operation: {Operation}, Duration: {Duration}ms, Predicate: {Predicate}",
                    entityType, "FindAllAsync", stopwatch.ElapsedMilliseconds, match.ToString());
                throw;
            }
        }

        /// <summary>
        /// Marks an entity for deletion without saving
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        public virtual void Delete(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
                
            Context.Set<T>().Remove(entity);
        }

        /// <summary>
        /// Marks an entity for deletion asynchronously without saving
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <returns>Task representing the operation</returns>
        public virtual Task<int> DeleteAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
                
            Context.Set<T>().Remove(entity);
            return Task.FromResult(1); // Indicate one entity marked for deletion
        }

        /// <summary>
        /// Updates an entity in the context without saving
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <param name="key">The entity key</param>
        /// <returns>The updated entity or null if not found</returns>
        public virtual T Update(T entity, object key)
        {
            if (entity == null)
                return null;
                
            var existing = Context.Set<T>().Find(key);
            if (existing != null)
            {
                Context.Entry(existing).CurrentValues.SetValues(entity);
                return existing;
            }

            return null;
        }

        /// <summary>
        /// Updates an entity in the context asynchronously without saving
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <param name="key">The entity key</param>
        /// <returns>The updated entity or null if not found</returns>
        public virtual async Task<T> UpdateAsync(T entity, object key)
        {
            if (entity == null)
                return null;
                
            var existing = await Context.Set<T>().FindAsync(key);
            if (existing != null)
            {
                Context.Entry(existing).CurrentValues.SetValues(entity);
                return existing;
            }

            return null;
        }

        /// <summary>
        /// Gets the count of entities
        /// </summary>
        /// <returns>The total count</returns>
        public virtual int Count()
        {
            var stopwatch = Stopwatch.StartNew();
            var entityType = typeof(T).Name;
            
            Logger.LogInformation("Database Read Operation Started - Entity: {EntityType}, Operation: {Operation}",
                entityType, "Count");

            try
            {
                var result = Context.Set<T>().Count();
                stopwatch.Stop();
                
                Logger.LogInformation("Database Read Operation Completed - Entity: {EntityType}, Operation: {Operation}, Duration: {Duration}ms, TotalCount: {TotalCount}",
                    entityType, "Count", stopwatch.ElapsedMilliseconds, result);
                
                return result;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                Logger.LogError(ex, "Database Read Operation Failed - Entity: {EntityType}, Operation: {Operation}, Duration: {Duration}ms",
                    entityType, "Count", stopwatch.ElapsedMilliseconds);
                throw;
            }
        }

        /// <summary>
        /// Gets the count of entities asynchronously
        /// </summary>
        /// <returns>The total count</returns>
        public virtual async Task<int> CountAsync()
        {
            var stopwatch = Stopwatch.StartNew();
            var entityType = typeof(T).Name;
            
            Logger.LogInformation("Database Read Operation Started - Entity: {EntityType}, Operation: {Operation}",
                entityType, "CountAsync");

            try
            {
                var result = await Context.Set<T>().CountAsync();
                stopwatch.Stop();
                
                Logger.LogInformation("Database Read Operation Completed - Entity: {EntityType}, Operation: {Operation}, Duration: {Duration}ms, TotalCount: {TotalCount}",
                    entityType, "CountAsync", stopwatch.ElapsedMilliseconds, result);
                
                return result;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                Logger.LogError(ex, "Database Read Operation Failed - Entity: {EntityType}, Operation: {Operation}, Duration: {Duration}ms",
                    entityType, "CountAsync", stopwatch.ElapsedMilliseconds);
                throw;
            }
        }

        /// <summary>
        /// Saves all pending changes in the context
        /// </summary>
        public virtual void Save()
        {
            Context.SaveChanges();
        }

        /// <summary>
        /// Saves all pending changes in the context asynchronously
        /// </summary>
        /// <returns>The number of state entries written to the database</returns>
        public virtual async Task<int> SaveAsync()
        {
            return await Context.SaveChangesAsync();
        }

        /// <summary>
        /// Gets entities matching the predicate as a queryable
        /// </summary>
        /// <param name="predicate">The predicate to match</param>
        /// <returns>Queryable of matching entities</returns>
        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            var stopwatch = Stopwatch.StartNew();
            var entityType = typeof(T).Name;
            
            Logger.LogInformation("Database Read Operation Started - Entity: {EntityType}, Operation: {Operation}, Predicate: {Predicate}",
                entityType, "FindBy", predicate.ToString());

            try
            {
                var result = Context.Set<T>().Where(predicate);
                stopwatch.Stop();
                
                Logger.LogInformation("Database Read Operation Completed - Entity: {EntityType}, Operation: {Operation}, Duration: {Duration}ms (Queryable)",
                    entityType, "FindBy", stopwatch.ElapsedMilliseconds);
                
                return result;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                Logger.LogError(ex, "Database Read Operation Failed - Entity: {EntityType}, Operation: {Operation}, Duration: {Duration}ms, Predicate: {Predicate}",
                    entityType, "FindBy", stopwatch.ElapsedMilliseconds, predicate.ToString());
                throw;
            }
        }

        /// <summary>
        /// Gets entities matching the predicate as a collection asynchronously
        /// </summary>
        /// <param name="predicate">The predicate to match</param>
        /// <returns>Collection of matching entities</returns>
        public virtual async Task<ICollection<T>> FindByAsync(Expression<Func<T, bool>> predicate)
        {
            var stopwatch = Stopwatch.StartNew();
            var entityType = typeof(T).Name;
            
            Logger.LogInformation("Database Read Operation Started - Entity: {EntityType}, Operation: {Operation}, Predicate: {Predicate}",
                entityType, "FindByAsync", predicate.ToString());

            try
            {
                var result = await Context.Set<T>().Where(predicate).ToListAsync();
                stopwatch.Stop();
                
                Logger.LogInformation("Database Read Operation Completed - Entity: {EntityType}, Operation: {Operation}, Duration: {Duration}ms, RecordCount: {RecordCount}",
                    entityType, "FindByAsync", stopwatch.ElapsedMilliseconds, result.Count);
                
                return result;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                Logger.LogError(ex, "Database Read Operation Failed - Entity: {EntityType}, Operation: {Operation}, Duration: {Duration}ms, Predicate: {Predicate}",
                    entityType, "FindByAsync", stopwatch.ElapsedMilliseconds, predicate.ToString());
                throw;
            }
        }

        /// <summary>
        /// Gets all entities with specified includes
        /// </summary>
        /// <param name="includeProperties">Properties to include</param>
        /// <returns>Queryable with includes</returns>
        public virtual IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            var stopwatch = Stopwatch.StartNew();
            var entityType = typeof(T).Name;
            
            Logger.LogInformation("Database Read Operation Started - Entity: {EntityType}, Operation: {Operation}, IncludeCount: {IncludeCount}",
                entityType, "GetAllIncluding", includeProperties.Length);

            try
            {
                var queryable = GetAll();
                var result = includeProperties.Aggregate(queryable, (current, includeProperty) => current.Include(includeProperty));
                stopwatch.Stop();
                
                Logger.LogInformation("Database Read Operation Completed - Entity: {EntityType}, Operation: {Operation}, Duration: {Duration}ms, IncludeCount: {IncludeCount} (Queryable)",
                    entityType, "GetAllIncluding", stopwatch.ElapsedMilliseconds, includeProperties.Length);
                
                return result;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                Logger.LogError(ex, "Database Read Operation Failed - Entity: {EntityType}, Operation: {Operation}, Duration: {Duration}ms, IncludeCount: {IncludeCount}",
                    entityType, "GetAllIncluding", stopwatch.ElapsedMilliseconds, includeProperties.Length);
                throw;
            }
        }
    }
}