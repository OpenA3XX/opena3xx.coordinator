using System;

namespace OpenA3XX.Core.Configuration
{
    public static class CoordinatorConfiguration
    {
        public static string GetDatabasesFolderPath(OpenA3XXDatabase database)
        {
            //var databasesPath = Environment.GetEnvironmentVariable("opena3xx.database.path");
            var databasesPath = "C:\\Users\\david\\git\\opena3xx\\opena3xx.database";
            //var databasesPath = "/home/davidbonnici1984/git/opena3xx/opena3xx.database";
            if (string.IsNullOrWhiteSpace(databasesPath))
                throw new ApplicationException("opena3xx.database.path Environment Variable not set.");

            if (database == OpenA3XXDatabase.Core)
                return $"Data Source = {databasesPath}\\hardware.db";
            //return $"Data Source = {databasesPath}/hardware.db";

            throw new ApplicationException("Database file path not defined");
        }
    }
}