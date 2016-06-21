using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureBus.Sender
{
    public class Program
    {
        public static string connectionString = "Endpoint=sb://suhumar.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=n66dhzSK35onQr0gGV5wDwLhrWMDsPm5ZJjn1TchFbo=";
        public static string queueName = "queue1";
        public static void Main(string[] args)
        {
            var queueClient = QueueClient.CreateFromConnectionString(connectionString, queueName);

            for (int i = 0; i < 10; i++)
            {
                var message = new BrokeredMessage("Message :" + i);
                queueClient.Send(message);
                Console.WriteLine("Sent: " + i);
            }

            Console.Read();

            queueClient.Close();
        }
    }
}
