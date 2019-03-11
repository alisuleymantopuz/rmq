using EasyNetQ;
using RMQ.Domain.Order;
using RMQ.Infra;
using RMQ.Messaging;
using System;
using System.Drawing;
using System.Threading.Tasks;
using Console = Colorful.Console;

namespace RMQ.Registration.Service
{
    class Program
    {
        public static void Main(string[] args)
        {
            var consoleConfiguration = new ConsoleConfiguration().Get();

            using (var bus = RabbitHutch.CreateBus(consoleConfiguration[RabbitMqServer.ConStr]))
            {
                bus.SubscribeAsync<RegisterOrder>(RabbitMqServer.Registration.OrderQueue, message => Task.Factory.StartNew(() =>
                {
                    Console.WriteLine("Payment = <" +
                                      message.CardNumber + ", " +
                                      message.CardHolderName + ", " +
                                      message.ExpiryDate + ", " +
                                      message.Amount + ">", Color.Lime);

                    var cardPaymentRequest = new CardPaymentRequest()
                    {
                        Amount = message.Amount,
                        CardHolderName = message.CardHolderName,
                        CardNumber = message.CardNumber,
                        ExpiryDate = message.ExpiryDate
                    };

                    var paymentResponse = bus.Request<CardPaymentRequest, CardPaymentResponse>(cardPaymentRequest);

                    var orderNotification = new OrderNotification()
                    {
                        Address = message.Address,
                        AuthCode = paymentResponse.AuthCode,
                        DeliveryDate = DateTime.Now.AddDays(14),
                        Email = message.Email,
                        Name = message.Name
                    };

                    bus.Publish(orderNotification, cfg => cfg.WithQueueName(RabbitMqServer.Notification.NotificationQueue));

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
