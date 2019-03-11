using EasyNetQ;
using RMQ.Domain.Order;
using RMQ.Infra;
using RMQ.Messaging;
using System.Drawing;
using System.Threading.Tasks;
using Console = Colorful.Console;

namespace RMQ.Notification.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            var consoleConfiguration = new ConsoleConfiguration().Get();

            using (var bus = RabbitHutch.CreateBus(consoleConfiguration[RabbitMqServer.ConStr]))
            {
                bus.SubscribeAsync<OrderNotification>(RabbitMqServer.Notification.NotificationQueue, message => Task.Factory.StartNew(() =>
                {
                    Console.WriteLine("Notification! = <" +
                                      message.Address + ", " +
                                      message.AuthCode + ", " +
                                      message.DeliveryDate + ", " +
                                      message.Name + ", " +
                                      message.Email + ">", Color.Yellow);

                    Console.WriteLine("Notification mail has been sent!", Color.Yellow);

                }).ContinueWith(task =>
                {
                    if (task.IsCompleted && !task.IsFaulted)
                    {
                        Console.WriteLine("Finished processing all messages", Color.Gray);
                    }
                    else
                    {
                        throw new EasyNetQException("Message processing exception - look in the default error queue (broker)");
                    }
                }));

                Console.WriteLine("Listening for messages. Hit <return> to quit.");
                Console.ReadLine();
            }
        }
    }
}
