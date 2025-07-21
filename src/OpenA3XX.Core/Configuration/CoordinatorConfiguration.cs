using System;
using System.IO;

namespace OpenA3XX.Core.Configuration
{
    /// <summary>
    /// Configuration management for OpenA3XX Coordinator application.
    /// Provides centralized access to database connection strings and other configuration values.
    /// </summary>
    public static class CoordinatorConfiguration
    {
        private const string DefaultDatabasePath = "Data";
        private const string DatabaseEnvironmentVariable = "OPENA3XX_DATABASE_PATH";
        private const string CoreDatabaseFileName = "hardware.db";

        /// <summary>
        /// Gets the database connection string for the specified OpenA3XX database.
        /// </summary>
        /// <param name="database">The database type to get connection string for</param>
        /// <param name="configurationPath">Optional path from configuration</param>
        /// <returns>SQLite connection string</returns>
        /// <exception cref="ArgumentException">Thrown when database type is not supported</exception>
        /// <exception cref="InvalidOperationException">Thrown when database path cannot be determined</exception>
        public static string GetDatabasesFolderPath(OpenA3XXDatabase database, string configurationPath = null)
        {
            var databasePath = GetDatabasePath(configurationPath);
            
            return database switch
            {
                OpenA3XXDatabase.Core => $"Data Source={Path.Combine(databasePath, CoreDatabaseFileName)}",
                _ => throw new ArgumentException($"Database type '{database}' is not supported", nameof(database))
            };
        }

        /// <summary>
        /// Gets the database directory path from environment variables, configuration, or default location.
        /// Creates the directory if it doesn't exist.
        /// </summary>
        /// <param name="configurationPath">Optional path from configuration</param>
        /// <returns>The database directory path</returns>
        /// <exception cref="InvalidOperationException">Thrown when database path cannot be created or accessed</exception>
        private static string GetDatabasePath(string configurationPath = null)
        {
            string databasePath = null;
            
            // Priority order: 1. Environment variable, 2. Configuration, 3. Default
            if (!string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable(DatabaseEnvironmentVariable)))
            {
                databasePath = Environment.GetEnvironmentVariable(DatabaseEnvironmentVariable);
            }
            else if (!string.IsNullOrWhiteSpace(configurationPath))
            {
                databasePath = configurationPath;
            }
            else
            {
                var appDirectory = AppDomain.CurrentDomain.BaseDirectory;
                databasePath = Path.Combine(appDirectory, DefaultDatabasePath);
            }

            try
            {
                // Ensure the directory exists
                if (!Directory.Exists(databasePath))
                {
                    Directory.CreateDirectory(databasePath);
                }
                
                return databasePath;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    $"Unable to create or access database directory at '{databasePath}'. " +
                    $"Please ensure the application has proper permissions or set the '{DatabaseEnvironmentVariable}' environment variable to a valid path.",
                    ex);
            }
        }



        /// <summary>
        /// Gets the environment variable name used for database path configuration.
        /// </summary>
        public static string DatabasePathEnvironmentVariable => DatabaseEnvironmentVariable;
    }
}