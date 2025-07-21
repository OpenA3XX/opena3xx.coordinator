using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
        /// Initializes a new instance of the BaseRepository
        /// </summary>
        /// <param name="context">The database context</param>
        protected BaseRepository(DbContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Gets a queryable collection of all entities
        /// </summary>
        /// <returns>IQueryable of entities</returns>
        public virtual IQueryable<T> GetAll()
        {
            return Context.Set<T>();
        }

        /// <summary>
        /// Gets all entities as a list asynchronously
        /// </summary>
        /// <returns>Collection of all entities</returns>
        public virtual async Task<ICollection<T>> GetAllAsync()
        {
            return await Context.Set<T>().ToListAsync();
        }

        /// <summary>
        /// Gets an entity by its ID
        /// </summary>
        /// <param name="id">The entity ID</param>
        /// <returns>The entity or null if not found</returns>
        public virtual T Get(int id)
        {
            return Context.Set<T>().Find(id);
        }

        /// <summary>
        /// Gets an entity by its ID asynchronously
        /// </summary>
        /// <param name="id">The entity ID</param>
        /// <returns>The entity or null if not found</returns>
        public virtual async Task<T> GetAsync(int id)
        {
            return await Context.Set<T>().FindAsync(id);
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
            return Context.Set<T>().SingleOrDefault(match);
        }

        /// <summary>
        /// Finds a single entity matching the predicate asynchronously
        /// </summary>
        /// <param name="match">The predicate to match</param>
        /// <returns>The entity or null if not found</returns>
        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> match)
        {
            return await Context.Set<T>().SingleOrDefaultAsync(match);
        }

        /// <summary>
        /// Finds all entities matching the predicate
        /// </summary>
        /// <param name="match">The predicate to match</param>
        /// <returns>Collection of matching entities</returns>
        public virtual ICollection<T> FindAll(Expression<Func<T, bool>> match)
        {
            return Context.Set<T>().Where(match).ToList();
        }

        /// <summary>
        /// Finds all entities matching the predicate asynchronously
        /// </summary>
        /// <param name="match">The predicate to match</param>
        /// <returns>Collection of matching entities</returns>
        public virtual async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match)
        {
            return await Context.Set<T>().Where(match).ToListAsync();
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
            return Context.Set<T>().Count();
        }

        /// <summary>
        /// Gets the count of entities asynchronously
        /// </summary>
        /// <returns>The total count</returns>
        public virtual async Task<int> CountAsync()
        {
            return await Context.Set<T>().CountAsync();
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
            return Context.Set<T>().Where(predicate);
        }

        /// <summary>
        /// Gets entities matching the predicate as a collection asynchronously
        /// </summary>
        /// <param name="predicate">The predicate to match</param>
        /// <returns>Collection of matching entities</returns>
        public virtual async Task<ICollection<T>> FindByAsync(Expression<Func<T, bool>> predicate)
        {
            return await Context.Set<T>().Where(predicate).ToListAsync();
        }

        /// <summary>
        /// Gets all entities with specified includes
        /// </summary>
        /// <param name="includeProperties">Properties to include</param>
        /// <returns>Queryable with includes</returns>
        public virtual IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            var queryable = GetAll();
            return includeProperties.Aggregate(queryable, (current, includeProperty) => current.Include(includeProperty));
        }
    }
}