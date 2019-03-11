using EasyNetQ;
using RMQ.Domain.Order;
using RMQ.Infra;
using RMQ.Messaging;
using System.Threading.Tasks;
using Console = Colorful.Console;

namespace RMQ.Payment.Service
{
    class Program
    {
        static void Main(string[] args)
        {

            var configuration = new ConsoleConfiguration().Get();

            var cardPaymentRequestWorkerPopulator = new PaymentWorkerCollection();

            var workers = cardPaymentRequestWorkerPopulator.Populate();

            using (var bus = RabbitHutch.CreateBus(configuration[RabbitMqServer.ConStr]))
            {
                bus.RespondAsync<CardPaymentRequest, CardPaymentResponse>(request =>
                {
                    return Task.Factory.StartNew(() =>
                    {
                        var worker = workers.Take();
                        try
                        {
                            return worker.Execute(request);
                        }
                        finally
                        {
                            workers.Add(worker);
                        }
                    });
                });

                Console.WriteLine("Listening for messages. Hit <return> to quit.");

                Console.ReadLine();
            }
        }
    }
}
