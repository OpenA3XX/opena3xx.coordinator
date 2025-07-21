using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using OpenA3XX.Core.Dtos;
using OpenA3XX.Core.Exceptions;

namespace OpenA3XX.Peripheral.WebApi.Middleware
{
    /// <summary>
    /// Global exception handling middleware that provides consistent error responses
    /// and proper logging for all unhandled exceptions
    /// </summary>
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;
        
        public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Invokes the middleware to handle exceptions
        /// </summary>
        /// <param name="context">The HTTP context</param>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Handles exceptions and provides appropriate responses
        /// </summary>
        /// <param name="context">The HTTP context</param>
        /// <param name="exception">The exception that occurred</param>
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var (statusCode, errorResponse) = exception switch
            {
                HardwareInputTypeExistsException ex => HandleValidationException(ex),
                HardwareOutputTypeExistsException ex => HandleValidationException(ex),
                ValidationException ex => HandleValidationExceptionWithDetails(ex),
                EntityNotFoundException ex => HandleEntityNotFoundException(ex),
                ArgumentNullException ex => HandleArgumentNullException(ex),
                ArgumentException ex => HandleArgumentException(ex),
                InvalidOperationException ex => HandleInvalidOperationException(ex),
                UnauthorizedAccessException ex => HandleUnauthorizedException(ex),
                NotImplementedException ex => HandleNotImplementedException(ex),
                _ => HandleGenericException(exception)
            };

            _logger.LogError(exception, "An error occurred while processing request {Method} {Path}. StatusCode: {StatusCode}",
                context.Request.Method, context.Request.Path, statusCode);

            // Set the path in the error response
            errorResponse.Path = context.Request.Path;

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var jsonResponse = JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await context.Response.WriteAsync(jsonResponse);
        }

        /// <summary>
        /// Handles validation exceptions (business rule violations)
        /// </summary>
        private static (HttpStatusCode, ErrorDto) HandleValidationException(Exception ex)
        {
            return (HttpStatusCode.BadRequest, ErrorDto.Create(ex.Message, "VALIDATION_ERROR"));
        }

        /// <summary>
        /// Handles argument exceptions (invalid parameters)
        /// </summary>
        private static (HttpStatusCode, ErrorDto) HandleArgumentException(ArgumentException ex)
        {
            return (HttpStatusCode.BadRequest, ErrorDto.Create($"Invalid argument: {ex.Message}", "INVALID_ARGUMENT"));
        }

        /// <summary>
        /// Handles null argument exceptions
        /// </summary>
        private static (HttpStatusCode, ErrorDto) HandleArgumentNullException(ArgumentNullException ex)
        {
            return (HttpStatusCode.BadRequest, ErrorDto.Create($"Required parameter is missing: {ex.ParamName}", "MISSING_PARAMETER"));
        }

        /// <summary>
        /// Handles invalid operation exceptions
        /// </summary>
        private static (HttpStatusCode, ErrorDto) HandleInvalidOperationException(InvalidOperationException ex)
        {
            return (HttpStatusCode.BadRequest, ErrorDto.Create($"Invalid operation: {ex.Message}", "INVALID_OPERATION"));
        }

        /// <summary>
        /// Handles unauthorized access exceptions
        /// </summary>
        private static (HttpStatusCode, ErrorDto) HandleUnauthorizedException(UnauthorizedAccessException ex)
        {
            return (HttpStatusCode.Unauthorized, ErrorDto.Create("Access denied", "UNAUTHORIZED"));
        }

        /// <summary>
        /// Handles not implemented exceptions
        /// </summary>
        private static (HttpStatusCode, ErrorDto) HandleNotImplementedException(NotImplementedException ex)
        {
            return (HttpStatusCode.NotImplemented, ErrorDto.Create("This feature is not yet implemented", "NOT_IMPLEMENTED"));
        }

        /// <summary>
        /// Handles ValidationException with detailed error information
        /// </summary>
        private static (HttpStatusCode, ErrorDto) HandleValidationExceptionWithDetails(ValidationException ex)
        {
            var details = ex.ValidationErrors?.ToDictionary(
                kvp => kvp.Key, 
                kvp => (object)kvp.Value
            );

            return (HttpStatusCode.BadRequest, ErrorDto.Create(ex.Message, "VALIDATION_ERROR", details));
        }

        /// <summary>
        /// Handles EntityNotFoundException
        /// </summary>
        private static (HttpStatusCode, ErrorDto) HandleEntityNotFoundException(EntityNotFoundException ex)
        {
            var details = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(ex.EntityType))
                details["entityType"] = ex.EntityType;
            if (ex.EntityId != null)
                details["entityId"] = ex.EntityId;

            return (HttpStatusCode.NotFound, ErrorDto.Create(ex.Message, "ENTITY_NOT_FOUND", details));
        }

        /// <summary>
        /// Handles all other exceptions with a generic response
        /// </summary>
        private static (HttpStatusCode, ErrorDto) HandleGenericException(Exception ex)
        {
            return (HttpStatusCode.InternalServerError, ErrorDto.Create(
                "An unexpected error occurred. Please contact support if the problem persists.", 
                "INTERNAL_SERVER_ERROR"));
        }
    }
} 