using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventStore.Client;
using EventStore;

namespace Documentation
{
    internal class Class1
    {
        public void Boi()
        {
            string connectionString = "esdb://127.0.0.1:2113?tls=false&keepAliveTimeout=10000&keepAliveInterval=10000";
            var clientSettings = EventStoreClientSettings.Create(connectionString);
            var client = new EventStorePersistentSubscriptionsClient(clientSettings);

            //client.su
            //client

        }

        
    }
}
