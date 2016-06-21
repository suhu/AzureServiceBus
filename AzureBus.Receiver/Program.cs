using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureBus.Receiver
{
    public class Program
    {
        public static string connectionString = "Endpoint=sb://suhumar.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=n66dhzSK35onQr0gGV5wDwLhrWMDsPm5ZJjn1TchFbo=";
        public static string queueName = "queue1";

        public static void Main(string[] args)
        {
            var queueClient = QueueClient.CreateFromConnectionString(connectionString, queueName);

            queueClient.OnMessage(ProcessMessage);

            Console.WriteLine("Press enter to exit");
            Console.ReadLine();
        }

        private static void  ProcessMessage(BrokeredMessage message)
        {
            var body = message.GetBody<string>();
            Console.WriteLine("Message body: " + body);
        }

         
    }
}
