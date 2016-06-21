using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace AzureBus.Chat
{
    public class Program
    {
        public static string connectionString = "Endpoint=sb://suhumar.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=n66dhzSK35onQr0gGV5wDwLhrWMDsPm5ZJjn1TchFbo=";
        public static string topicPath = "chat";

        public static void Main(string[] args)
        {

            Console.WriteLine("Enter name:");
            var usernamae = Console.ReadLine();
            var manager = NamespaceManager.CreateFromConnectionString(connectionString);
            if (!manager.TopicExists(topicPath))
            {
                manager.CreateTopic(topicPath);
            }

            var subscriptiondesc = new SubscriptionDescription(topicPath, usernamae)
            {
                AutoDeleteOnIdle = TimeSpan.FromMinutes(10)
            };

            manager.CreateSubscription(subscriptiondesc);

            var topicClient = TopicClient.CreateFromConnectionString(connectionString, topicPath);
            var subscriptionClient = SubscriptionClient.CreateFromConnectionString(connectionString, topicPath, usernamae);

            subscriptionClient.OnMessage(ProcessMessage);

            var welcomeMessage = new BrokeredMessage("Has entered the room");
            welcomeMessage.Label = usernamae;
            topicClient.Send(welcomeMessage);

            while(true)
            {
                var message = Console.ReadLine();
                if (message == "exit") break;

                var chatMessage = new BrokeredMessage(message);
                chatMessage.Label = usernamae;
                topicClient.Send(chatMessage);
            }

            topicClient.Close();
            subscriptionClient.Close();
           
         }

        private static void ProcessMessage(BrokeredMessage message)
        {
            var user = message.Label;
            var body = message.GetBody<String>();
            Console.WriteLine(user + ">" + body);
        }
    }
    
}
