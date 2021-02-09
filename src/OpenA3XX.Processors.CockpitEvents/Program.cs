using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OpenA3XX.Core.Configuration;
using OpenA3XX.Core.DataContexts;
using OpenA3XX.Core.Models;
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
/*
            var repo = new ManufacturerRepository(new CoreDataContext(dbContextOptionsBuilder.Options));

            var data = repo.GetAllManufacturers();


            var adata = JsonConvert.SerializeObject(data, Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
*/
            var hardwareBoard = new HardwareBoard
            {
                Name = "MCDU1",
                Buses = new List<IOExtenderBus>
                {
                    new IOExtenderBus()
                    {
                        HardwareBus = HardwareBus.Bus0,
                        Bits = new List<IOExtenderBit>()
                        {
                            new IOExtenderBit
                            {
                                ExtenderBusBitType = ExtenderBusBitType.Input, HardwareInputId = 13
                            },
                            new IOExtenderBit
                            {
                                ExtenderBusBitType = ExtenderBusBitType.Input, HardwareInputId = 14
                            },
                            new IOExtenderBit
                            {
                                ExtenderBusBitType = ExtenderBusBitType.Input, HardwareInputId = 15
                            },
                            new IOExtenderBit
                            {
                                ExtenderBusBitType = ExtenderBusBitType.Input, HardwareInputId = 16
                            }
                        }
                    }
                }
            };



            var repo = new HardwareBoardRepository(new CoreDataContext(dbContextOptionsBuilder.Options));
           // var x = repo.AddHardwareBoard(hardwareBoard);
           var data = repo.GetAll()
               .Include(c => c.Buses)
               .ThenInclude(c => c.Bits)
               .ThenInclude(c => c.HardwareInput)
               .ThenInclude(c => c.HardwareInputType)
               .Include(c => c.Buses)
               .ThenInclude(c => c.Bits)
               .ThenInclude(c => c.HardwareOutput)
               .ThenInclude(c => c.HardwareOutputType)
               .ToList();

            Console.ReadLine();
        }
    }

    public static class ExtMethods
    {
        public static IQueryable<T> IncludeAll<T>(this IQueryable<T> queryable) where T : class
        {
            var type = typeof(T);
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                var isVirtual = property.GetGetMethod().IsVirtual;
                if (isVirtual && properties.FirstOrDefault(c => c.Name == property.Name + "Id") != null)
                {
                    queryable = queryable.Include(property.Name);
                }
            }

            return queryable;
        }
    }
}