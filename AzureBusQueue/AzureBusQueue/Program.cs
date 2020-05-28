using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace AzureBusQueue
{
    class Program
    {
        const string ServiceBusConnectionString = "";
        const string QueueName = "";
        static IQueueClient queueClient;
        public static async Task Main()
        {
            const int numberofmessages = 10;
            queueClient = new QueueClient(ServiceBusConnectionString,QueueName);
            Console.WriteLine("=======================================");
            Console.WriteLine("Press enter KEY after exit to send all meassages ");
            Console.WriteLine("=======================================");
            //send mesages
            await SendMessageAynsc(numberofmessages);
            Console.ReadKey();
            await queueClient.CloseAsync();

        }
        static async Task SendMessageAynsc(int numberofmessagestosend)
        {
            try
            {
                for (var i = 0; i < numberofmessagestosend; i++)
                {
                    string messgaeBody = $"messgae{i}";
                    var message = new Message(Encoding.UTF8.GetBytes(messgaeBody));
                    Console.WriteLine($"sending message:{messgaeBody}");
                    await queueClient.SendAsync(message);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{DateTime.Now} :: Exception: {exception.Message}");
            }
        }
    }
}
