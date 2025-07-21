using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenA3XX.Core.Exceptions
{
    /// <summary>
    /// Exception thrown when validation fails for business rules or input data
    /// </summary>
    [Serializable]
    public class ValidationException : Exception
    {
        /// <summary>
        /// Collection of validation errors
        /// </summary>
        public IReadOnlyDictionary<string, string[]> ValidationErrors { get; }

        /// <summary>
        /// Initializes a new instance of ValidationException
        /// </summary>
        public ValidationException() : base("One or more validation errors occurred")
        {
            ValidationErrors = new Dictionary<string, string[]>();
        }

        /// <summary>
        /// Initializes a new instance of ValidationException with a custom message
        /// </summary>
        /// <param name="message">The exception message</param>
        public ValidationException(string message) : base(message)
        {
            ValidationErrors = new Dictionary<string, string[]>();
        }

        /// <summary>
        /// Initializes a new instance of ValidationException with validation errors
        /// </summary>
        /// <param name="validationErrors">Dictionary of field names and their validation errors</param>
        public ValidationException(IDictionary<string, string[]> validationErrors)
            : base("One or more validation errors occurred")
        {
            ValidationErrors = validationErrors?.ToDictionary(kvp => kvp.Key, kvp => kvp.Value) 
                              ?? new Dictionary<string, string[]>();
        }

        /// <summary>
        /// Initializes a new instance of ValidationException with a single field error
        /// </summary>
        /// <param name="fieldName">The name of the field with validation error</param>
        /// <param name="errorMessage">The validation error message</param>
        public ValidationException(string fieldName, string errorMessage)
            : base($"Validation failed for field '{fieldName}': {errorMessage}")
        {
            ValidationErrors = new Dictionary<string, string[]>
            {
                [fieldName] = new[] { errorMessage }
            };
        }

        /// <summary>
        /// Initializes a new instance of ValidationException with an inner exception
        /// </summary>
        /// <param name="message">The exception message</param>
        /// <param name="innerException">The inner exception</param>
        public ValidationException(string message, Exception innerException) : base(message, innerException)
        {
            ValidationErrors = new Dictionary<string, string[]>();
        }
    }
} 