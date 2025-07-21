using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OpenA3XX.Core.Repositories.Base
{
    /// <summary>
    /// Base repository interface defining common data access operations
    /// </summary>
    /// <typeparam name="T">The entity type</typeparam>
    public interface IBaseRepository<T> where T : class
    {
        /// <summary>
        /// Adds an entity to the context without saving
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <returns>The added entity</returns>
        T Add(T entity);
        
        /// <summary>
        /// Adds an entity to the context asynchronously without saving
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <returns>The added entity</returns>
        Task<T> AddAsync(T entity);
        
        /// <summary>
        /// Gets the count of entities
        /// </summary>
        /// <returns>The total count</returns>
        int Count();
        
        /// <summary>
        /// Gets the count of entities asynchronously
        /// </summary>
        /// <returns>The total count</returns>
        Task<int> CountAsync();
        
        /// <summary>
        /// Marks an entity for deletion without saving
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        void Delete(T entity);
        
        /// <summary>
        /// Marks an entity for deletion asynchronously without saving
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <returns>Task representing the operation</returns>
        Task<int> DeleteAsync(T entity);
        
        /// <summary>
        /// Finds a single entity matching the predicate
        /// </summary>
        /// <param name="match">The predicate to match</param>
        /// <returns>The entity or null if not found</returns>
        T Find(Expression<Func<T, bool>> match);
        
        /// <summary>
        /// Finds all entities matching the predicate
        /// </summary>
        /// <param name="match">The predicate to match</param>
        /// <returns>Collection of matching entities</returns>
        ICollection<T> FindAll(Expression<Func<T, bool>> match);
        
        /// <summary>
        /// Finds all entities matching the predicate asynchronously
        /// </summary>
        /// <param name="match">The predicate to match</param>
        /// <returns>Collection of matching entities</returns>
        Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match);
        
        /// <summary>
        /// Finds a single entity matching the predicate asynchronously
        /// </summary>
        /// <param name="match">The predicate to match</param>
        /// <returns>The entity or null if not found</returns>
        Task<T> FindAsync(Expression<Func<T, bool>> match);
        
        /// <summary>
        /// Gets entities matching the predicate as a queryable
        /// </summary>
        /// <param name="predicate">The predicate to match</param>
        /// <returns>Queryable of matching entities</returns>
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        
        /// <summary>
        /// Gets entities matching the predicate as a collection asynchronously
        /// </summary>
        /// <param name="predicate">The predicate to match</param>
        /// <returns>Collection of matching entities</returns>
        Task<ICollection<T>> FindByAsync(Expression<Func<T, bool>> predicate);
        
        /// <summary>
        /// Gets an entity by its ID
        /// </summary>
        /// <param name="id">The entity ID</param>
        /// <returns>The entity or null if not found</returns>
        T Get(int id);
        
        /// <summary>
        /// Gets a queryable collection of all entities
        /// </summary>
        /// <returns>IQueryable of entities</returns>
        IQueryable<T> GetAll();
        
        /// <summary>
        /// Gets all entities as a list asynchronously
        /// </summary>
        /// <returns>Collection of all entities</returns>
        Task<ICollection<T>> GetAllAsync();
        
        /// <summary>
        /// Gets all entities with specified includes
        /// </summary>
        /// <param name="includeProperties">Properties to include</param>
        /// <returns>Queryable with includes</returns>
        IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);
        
        /// <summary>
        /// Gets an entity by its ID asynchronously
        /// </summary>
        /// <param name="id">The entity ID</param>
        /// <returns>The entity or null if not found</returns>
        Task<T> GetAsync(int id);
        
        /// <summary>
        /// Saves all pending changes in the context
        /// </summary>
        void Save();
        
        /// <summary>
        /// Saves all pending changes in the context asynchronously
        /// </summary>
        /// <returns>The number of state entries written to the database</returns>
        Task<int> SaveAsync();
        
        /// <summary>
        /// Updates an entity in the context without saving
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <param name="key">The entity key</param>
        /// <returns>The updated entity or null if not found</returns>
        T Update(T entity, object key);
        
        /// <summary>
        /// Updates an entity in the context asynchronously without saving
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <param name="key">The entity key</param>
        /// <returns>The updated entity or null if not found</returns>
        Task<T> UpdateAsync(T entity, object key);
    }
}