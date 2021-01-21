using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenA3XX.Core.Models;
using OpenA3XX.Core.Repositories;

namespace OpenA3XX.Coordinator.TestHarness
{
    class Program
    {
        static void Main(string[] args)
        {
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<HardwareDataContext>();
            dbContextOptionsBuilder.UseSqlite(CoordinatorConfiguration.GetDatabasesFolderPath(OpenA3XXDatabase.Hardware));

            var repo = new HardwareComponentRepository(new HardwareDataContext(dbContextOptionsBuilder.Options));

            var data = repo.GetAllHardwareComponents();

            Console.ReadLine();
        }
    }
}