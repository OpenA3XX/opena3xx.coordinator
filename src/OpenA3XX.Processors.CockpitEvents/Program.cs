using System;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OpenA3XX.Core.Configuration;
using OpenA3XX.Core.DataContexts;
using OpenA3XX.Core.Repositories;

namespace OpenA3XX.Processors.CockpitEvents
{
    class Program
    {
        static void Main(string[] args)
        {
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<CoreDataContext>();
            dbContextOptionsBuilder.UseSqlite(
                CoordinatorConfiguration.GetDatabasesFolderPath(OpenA3XXDatabase.Core));

            var repo = new ManufacturerRepository(new CoreDataContext(dbContextOptionsBuilder.Options));

            var data = repo.GetAllManufacturers();


            var adata = JsonConvert.SerializeObject(data, Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

            Console.ReadLine();
        }
    }
}