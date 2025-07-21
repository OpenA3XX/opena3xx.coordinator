using System;
using System.Collections.Generic;

namespace OpenA3XX.Core.Dtos
{
    /// <summary>
    /// Data transfer object for error responses
    /// </summary>
    public class ErrorDto
    {
        /// <summary>
        /// The main error message
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// A specific error code for the error type
        /// </summary>
        public string ErrorCode { get; set; }

        /// <summary>
        /// Timestamp when the error occurred
        /// </summary>
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Additional details or context about the error
        /// </summary>
        public Dictionary<string, object> Details { get; set; }

        /// <summary>
        /// The request path where the error occurred
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Creates a simple error response with just a message
        /// </summary>
        /// <param name="message">The error message</param>
        /// <returns>ErrorDto with the specified message</returns>
        public static ErrorDto Create(string message)
        {
            return new ErrorDto { ErrorMessage = message };
        }

        /// <summary>
        /// Creates an error response with message and error code
        /// </summary>
        /// <param name="message">The error message</param>
        /// <param name="errorCode">The error code</param>
        /// <returns>ErrorDto with message and error code</returns>
        public static ErrorDto Create(string message, string errorCode)
        {
            return new ErrorDto 
            { 
                ErrorMessage = message, 
                ErrorCode = errorCode 
            };
        }

        /// <summary>
        /// Creates a detailed error response with additional context
        /// </summary>
        /// <param name="message">The error message</param>
        /// <param name="errorCode">The error code</param>
        /// <param name="details">Additional error details</param>
        /// <param name="path">The request path</param>
        /// <returns>ErrorDto with full error details</returns>
        public static ErrorDto Create(string message, string errorCode, Dictionary<string, object> details, string path = null)
        {
            return new ErrorDto 
            { 
                ErrorMessage = message, 
                ErrorCode = errorCode,
                Details = details,
                Path = path
            };
        }
    }
}