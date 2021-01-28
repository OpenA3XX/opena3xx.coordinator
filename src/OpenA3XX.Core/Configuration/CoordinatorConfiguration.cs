using System;

namespace OpenA3XX.Core.Configuration
{
    public static class CoordinatorConfiguration
    {
        public static string GetDatabasesFolderPath(OpenA3XXDatabase database)
        {
            var databasesPath = Environment.GetEnvironmentVariable("opena3xx.database.path");
            if (string.IsNullOrWhiteSpace(databasesPath))
            {
                throw new ApplicationException("opena3xx.database.path Environment Variable not set.");
            }

            if (database == OpenA3XXDatabase.Hardware)
            {
                return $"Data Source = {databasesPath}\\hardware.db";
            }

            throw new ApplicationException("Database file path not defined");
        }
    }

    public enum OpenA3XXDatabase
    {
        Hardware
    }
}