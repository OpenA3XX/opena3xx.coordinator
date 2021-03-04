using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OpenA3XX.Core.Configuration;
using OpenA3XX.Core.DataContexts;
using OpenA3XX.Core.Dtos;
using OpenA3XX.Core.Models;
using OpenA3XX.Core.Repositories;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace OpenA3XX.Processors.CockpitEvents
{
    internal class Program
    {
        private static void Main(string[] args)
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
                    new()
                    {
                        HardwareBus = HardwareBus.Bus0,
                        Bits = new List<IOExtenderBit>
                        {
                            new()
                            {
                                ExtenderBusBitType = ExtenderBusBitType.Input, HardwareInputId = 13
                            },
                            new()
                            {
                                ExtenderBusBitType = ExtenderBusBitType.Input, HardwareInputId = 14
                            },
                            new()
                            {
                                ExtenderBusBitType = ExtenderBusBitType.Input, HardwareInputId = 15
                            },
                            new()
                            {
                                ExtenderBusBitType = ExtenderBusBitType.Input, HardwareInputId = 16
                            }
                        }
                    }
                }
            };

            var factory = new ConnectionFactory
            {
                UserName = "opena3xx",
                Password = "opena3xx",
                VirtualHost = "/",
                HostName = "192.168.50.22",
                ClientProvidedName = "app:opena3xx.processors component:cockpitevents"
            };
            var conn = factory.CreateConnection();
            var channel = conn.CreateModel();

            channel.QueueDeclare("hardware_events", false, false, false, null);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (ch, ea) =>
            {
                channel.BasicAck(ea.DeliveryTag, false);
                var result = Encoding.UTF8.GetString(ea.Body.ToArray());
                var hardwareSignalDto = JsonConvert.DeserializeObject<HardwareSignalDto>(result);
                var hardwareBoardRepository =
                    new HardwareBoardRepository(new CoreDataContext(dbContextOptionsBuilder.Options));
                var response = hardwareBoardRepository.GetByHardwarePanel(hardwareSignalDto.HardwareBoardId);
            };

            var consumerTag = channel.BasicConsume("hardware_events", false, consumer);


            Console.ReadLine();
        }
    }
}