using System;
using System.Collections.Generic;

namespace OpenA3XX.Peripheral.WebApi.Hubs
{
    public class MessagingExchangeConfiguration
    {
        public MessagingExchangeConfiguration(string configurationString)
        {
            //opena3xx.hardware_boards.keep_alive>>admin.keepalive|KeepAlive,general.keepalive|NA
            
            ExchangeName = configurationString.Split(">>")[0]; //opena3xx.hardware_boards.keep_alive
            
            var queuesConfiguration = configurationString.Split(">>")[1]; //admin.keepalive|KeepAlive,general.keepalive|NA
            
            var queueList = queuesConfiguration.Split(","); //["admin.keepalive|KeepAlive","general.keepalive|NA]"]

            QueueSocketBindingConfiguration = new List<Tuple<string, string>>();
            
            foreach (var queues in queueList)
            {
                var queueName = queues.Split("|")[0];
                var signalrMethodName = queues.Split("|")[1];
                if (signalrMethodName == "NA")
                {
                    signalrMethodName = string.Empty;
                }
                QueueSocketBindingConfiguration.Add(new Tuple<string, string>(queueName, signalrMethodName));
            }
        }
        public string ExchangeName { get; set; }
    
        public IList<Tuple<string, string>> QueueSocketBindingConfiguration { get; set; }
    
    }
}