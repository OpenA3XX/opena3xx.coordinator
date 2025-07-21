using System;

namespace OpenA3XX.Core.Exceptions
{
    /// <summary>
    /// Exception thrown when a requested entity cannot be found
    /// </summary>
    [Serializable]
    public class EntityNotFoundException : Exception
    {
        /// <summary>
        /// The type of entity that was not found
        /// </summary>
        public string EntityType { get; }

        /// <summary>
        /// The identifier that was used to search for the entity
        /// </summary>
        public object EntityId { get; }

        /// <summary>
        /// Initializes a new instance of EntityNotFoundException
        /// </summary>
        public EntityNotFoundException() : base("The requested entity was not found")
        {
        }

        /// <summary>
        /// Initializes a new instance of EntityNotFoundException with a custom message
        /// </summary>
        /// <param name="message">The exception message</param>
        public EntityNotFoundException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of EntityNotFoundException with entity details
        /// </summary>
        /// <param name="entityType">The type of entity that was not found</param>
        /// <param name="entityId">The identifier that was used to search</param>
        public EntityNotFoundException(string entityType, object entityId) 
            : base($"{entityType} with ID '{entityId}' was not found")
        {
            EntityType = entityType;
            EntityId = entityId;
        }

        /// <summary>
        /// Initializes a new instance of EntityNotFoundException with entity details and custom message
        /// </summary>
        /// <param name="entityType">The type of entity that was not found</param>
        /// <param name="entityId">The identifier that was used to search</param>
        /// <param name="message">Custom message</param>
        public EntityNotFoundException(string entityType, object entityId, string message) : base(message)
        {
            EntityType = entityType;
            EntityId = entityId;
        }

        /// <summary>
        /// Initializes a new instance of EntityNotFoundException with an inner exception
        /// </summary>
        /// <param name="message">The exception message</param>
        /// <param name="innerException">The inner exception</param>
        public EntityNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
} 